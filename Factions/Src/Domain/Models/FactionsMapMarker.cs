namespace Oxide.Plugins
{
    using UnityEngine;
    public interface IFactionsMapMarkerSpecification
    {
        Vector2 GetLocation();
    }

    public sealed class ClanClaim : IFactionsMapMarkerSpecification
    {
        private readonly Vector2 _location;
        public readonly Color Color;
        public readonly bool IsCapital;

        public ClanClaim(float colorRed, float colorGreen, float colorBlue, bool isCapital, Vector2 location)
        {
            Color = new Color(colorRed, colorGreen, colorBlue);
            _location = location;
            IsCapital = isCapital;
        }

        Vector2 IFactionsMapMarkerSpecification.GetLocation()
        {
            return _location;
        }
    }

    public sealed class Badlands : IFactionsMapMarkerSpecification
    {
        private readonly Vector2 _location;

        public Badlands()
        {
            // TODO
        }

        Vector2 IFactionsMapMarkerSpecification.GetLocation()
        {
            return _location;
        }
    }

    public sealed class FactionsMapMarker
    {
        private MapMarkerGenericRadius _marker;

        private static class Constants
        {
            public const string EntityPrefab = "assets/prefabs/tools/map/genericradiusmarker.prefab";
            public const string MarkerUpdateRpcFunction = "MarkerUpdate";
            public const float ClanClaimAlpha = 0.5f;
        }

        public FactionsMapMarker(IFactionsMapMarkerSpecification specification)
        {
            Vector2 position = specification.GetLocation();
            _marker = GameManager.server.CreateEntity(Constants.EntityPrefab, position).GetComponent<MapMarkerGenericRadius>();

            var claim = (ClanClaim)specification;
            if (claim != null)
            {
                _marker.color1 = claim.Color;
                _marker.radius = 2f;
                _marker.alpha = Constants.ClanClaimAlpha;
            }

            _marker.Spawn();
        }

        public void Respawn()
        {
            var prevColor = _marker.color1;
            var prevRadius = _marker.radius;
            var prevAlpha = _marker.alpha;
            _marker.Kill();
            _marker = GameManager.server.CreateEntity(Constants.EntityPrefab, _marker.ServerPosition).GetComponent<MapMarkerGenericRadius>();
            _marker.color1 = prevColor;
            _marker.radius = prevRadius;
            _marker.alpha = prevAlpha;
            _marker.Spawn();
        }

        public void NetworkMarkerToPlayer(BasePlayer player)
        {
            var color = new Vector3(_marker.color1.r, _marker.color1.g, _marker.color1.b);
            _marker.ClientRPCPlayer<Vector3, float, Vector3, float, float>(
                sourceConnection: null,
                player: player,
                funcName: Constants.MarkerUpdateRpcFunction,
                arg1: color,
                arg2: _marker.alpha,
                arg3: color,
                arg4: _marker.alpha,
                arg5: _marker.radius);
        }

        public void Kill()
        {
            _marker.Kill();
        }
    }
}

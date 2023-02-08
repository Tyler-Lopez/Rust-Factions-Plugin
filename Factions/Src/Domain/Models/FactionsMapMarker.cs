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

    public sealed class FactionsMapMarker
    {
        private readonly MapMarkerGenericRadius _marker;

        private static class Constants
        {
            public const string EntityPrefab = "assets/prefabs/tools/map/genericradiusmarker.prefab";
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
            _marker.SendUpdate();
        }

        public BaseEntity GetMarkerEntity()
        {
            return _marker;
        }

        public Color GetColor()
        {
            return _marker.color1;
        }

        public void SendMarkerUpdate()
        {
            _marker.SendUpdate();
        }
    }

}

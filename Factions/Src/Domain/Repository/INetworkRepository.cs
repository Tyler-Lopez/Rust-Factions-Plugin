namespace Oxide.Plugins
{
    using UnityEngine;

    partial class Factions
    {
        private interface INetworkRepository
        {
            void SendMarkerToPlayer(
                BasePlayer player,
                float colorRed,
                float colorBlue,
                float colorGreen,
                float colorAlpha,
                Vector2 location,
                float radius
            );
        }
    }
}

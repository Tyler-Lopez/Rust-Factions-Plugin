namespace Oxide.Plugins
{
    using UnityEngine;

    partial class Factions
    {
        private interface INetworkRepository
        {
            void SendMarkerToPlayer(
                BasePlayer player,
                MapMarkerGenericRadius marker
            );
        }
    }
}

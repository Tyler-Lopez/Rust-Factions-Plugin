namespace Oxide.Plugins
{
    using UnityEngine;

    partial class Factions
    {
        private interface INetworkRepository
        {
            void AddMarkerToPlayerSubscription(
                BasePlayer player,
                FactionsMapMarker marker);

            void RemoveMarkerFromPlayerSubscription(
                BasePlayer player,
                FactionsMapMarker mapMarker);
        }
    }
}

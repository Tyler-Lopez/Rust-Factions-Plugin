
namespace Oxide.Plugins
{
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;
    using Facepunch;
    using Network;


    partial class Factions
    {
        private class NetworkRepository : INetworkRepository
        {
            void INetworkRepository.AddMarkerToPlayerSubscription(BasePlayer player, FactionsMapMarker marker)
            {
                marker.GetMarkerEntity().OnNetworkSubscribersEnter(new List<Connection>() { player.Connection });
                marker.SendMarkerUpdate();
            }

            void INetworkRepository.RemoveMarkerFromPlayerSubscription(BasePlayer player, FactionsMapMarker marker)
            {
                marker.GetMarkerEntity().OnNetworkSubscribersLeave(new List<Connection>() { player.Connection });
               marker.SendMarkerUpdate();
            }
        }
    }
}

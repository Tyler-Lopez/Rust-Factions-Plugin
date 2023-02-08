namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    partial class Factions
    {
        private FactionsMapMarker myMapMarker;

        [ChatCommand("test")]
        private void OnCommand(BasePlayer player, string command, string[] args)
        {
            myMapMarker = new FactionsMapMarker(new ClanClaim(
                colorRed: 1.0f,
                colorGreen: 1.0f,
                colorBlue: 1.0f,
                isCapital: false,
                location: Vector2.zero
            ));
        }

        [ChatCommand("test2")]
        private void OnCommandTwo(BasePlayer player, string command, string[] args)
        {
            _networkRepository.AddMarkerToPlayerSubscription(player, myMapMarker);
        }

        [ChatCommand("test3")]
        private void OnCommandThree(BasePlayer player, string command, string[] args)
        {
            _networkRepository.RemoveMarkerFromPlayerSubscription(player, myMapMarker);
        }
    }
}

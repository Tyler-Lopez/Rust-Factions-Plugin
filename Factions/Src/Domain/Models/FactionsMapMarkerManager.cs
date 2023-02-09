namespace Oxide.Plugins
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public class FactionsMapMarkerManager
    {
        private readonly Dictionary<string, FactionsMapMarker> _landClaimMarkers = new Dictionary<string, FactionsMapMarker>();
        private readonly HashSet<ulong> _playersHidingLandClaimMarkers = new HashSet<ulong>();

        // Add a new Land Claim type FactionsMapMarker to the Manager
        public void AddLandClaimMarker(string gridString, FactionsMapMarker marker)
        {
            // Kill and remove any previous marker, if it exists
            FactionsMapMarker prevMarker;
            _landClaimMarkers.TryGetValue(gridString, out prevMarker);
            prevMarker?.Kill();

            // Update tracked marker and network it to all online players
            _landClaimMarkers[gridString] = marker;
            foreach (var basePlayer in BasePlayer.activePlayerList)
            {
                if (!_playersHidingLandClaimMarkers.Contains(basePlayer.userID))
                {
                    marker.NetworkMarkerToPlayer(basePlayer);
                }
            }
        }

        // Networks all land claim markers to all players excluding 
        private void NetworkAllLandClaimMarkers(bool respawn = false)
        {
            foreach (var landClaimMarker in _landClaimMarkers.Values)
            {
                if (respawn) landClaimMarker.Respawn();
                foreach (var basePlayer in BasePlayer.activePlayerList)
                {
                    if (!_playersHidingLandClaimMarkers.Contains(basePlayer.userID))
                    {
                        landClaimMarker.NetworkMarkerToPlayer(basePlayer);
                    }
                }
            }
        }

        public void PlayerToggleLandClaimMarkerVisibility(BasePlayer player)
        {
            if (_playersHidingLandClaimMarkers.Contains(player.userID))
            {
                _playersHidingLandClaimMarkers.Remove(player.userID);
                foreach (var factionsMapMarker in _landClaimMarkers.Values)
                {
                    factionsMapMarker.NetworkMarkerToPlayer(player);
                }
            }
            else
            {
                _playersHidingLandClaimMarkers.Add(player.userID);
                NetworkAllLandClaimMarkers(respawn: true);
            }
        }

        public void DestroyMarkers()
        {
            foreach (var landClaimMarker in _landClaimMarkers.Values)
            {
                landClaimMarker.Kill();
            }
        }
    }
}

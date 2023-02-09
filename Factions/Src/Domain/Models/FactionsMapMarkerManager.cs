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

            // Update tracked marker reference
            _landClaimMarkers[gridString] = marker;
            
            // Network the new marker to all active players excluding those hiding land claims
            foreach (var basePlayer in BasePlayer.activePlayerList)
            {
                if (!_playersHidingLandClaimMarkers.Contains(basePlayer.userID))
                {
                    marker.NetworkMarkerToPlayer(basePlayer);
                }
            }
        }

        // Networks all land claim markers to all players excluding those hiding land claims
        private void NetworkAllLandClaimMarkers(bool respawn = false)
        {
            foreach (var landClaimMarker in _landClaimMarkers.Values)
            {
                // If necessary, respawn every land claim marker before networking them
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

        // Invoked when a player toggles their land claim marker visibility
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

        // Destroys all managed markers
        public void DestroyMarkers()
        {
            foreach (var landClaimMarker in _landClaimMarkers.Values)
            {
                landClaimMarker.Kill();
            }
        }
    }
}

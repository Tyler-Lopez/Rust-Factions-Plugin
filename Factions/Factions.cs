// Requires: ZoneManager

using System;
using System.Linq;
using Network;
using Oxide.Core.Plugins;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Factions", "Rust Factions", "1.0.0")]
    [Description("Pending Description")]
    public partial class Factions : RustPlugin
    {
        #region Global Variables
        private IZoneManagerRepository _zoneManagerRepository;
        private FactionsMapMarkerManager _factionsMapMarkerManager;

        public void Blah(BasePlayer player, Vector2 position)
        {
            /*
            Puts("Here in blah...");
            MapMarkerGenericRadius marker = GameManager.server.CreateEntity("assets/prefabs/tools/map/genericradiusmarker.prefab", position).GetComponent<MapMarkerGenericRadius>();
            marker.Spawn();
            Puts("spawned");
            var color = new Vector3(1f, 1f, 1f);
            marker.ClientRPCPlayer<Vector3, float, Vector3, float, float>((Connection)null, player, "MarkerUpdate", color, 50f, color, 1f, 50f);
            */
        }
        #endregion

        class MyMapMarker : MapMarker
        {

        }
    }
}
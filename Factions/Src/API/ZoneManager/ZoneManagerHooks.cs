namespace Oxide.Plugins
{
    using System;
    using UnityEngine;
    partial class Factions
    {
        private void OnEnterZone(string ZoneID, BasePlayer player)
        {
            _zoneManagerApi?.HandlePlayerEnterZone(ZoneID, player);
        }
    }
}

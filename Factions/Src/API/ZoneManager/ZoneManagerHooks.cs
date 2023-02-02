namespace Oxide.Plugins
{
    using System;
    using UnityEngine;
    partial class Factions
    {
        private void OnEnterZone(string ZoneID, BasePlayer player)
        {
            // Get the Grid the player has just entered
            Grid parseResult;
            if (!ZoneManagerApi.TryParseGrid(ZoneID, out parseResult)) return;

            // With that Grid, determine who, if anyone owns it

            // Invoke High Level Hook
        }
    }
}

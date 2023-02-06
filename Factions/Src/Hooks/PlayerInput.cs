namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    partial class Factions
    {
        [ChatCommand("test")]
        private void OnCommand(BasePlayer player, string command, string[] args)
        {
            Puts("Received input...");
            Blah(player, new Vector2(0, 0));
        }
    }
}

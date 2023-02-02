namespace Oxide.Plugins
{
    [Info("Factions", "Rust Factions", "1.0.0")]
    [Description("Pending Description")]
    public partial class Factions : RustPlugin
    {
        #region Global Variables
        private static Factions PluginInstance;
        #endregion

        public Factions()
        {
            PluginInstance = this;
        }
    }
}﻿﻿namespace Oxide.Plugins
{
    using System;
    using UnityEngine;
    partial class Factions
    {
        private void OnPlayerConnected(BasePlayer player)
        {
            Puts("Player connected!");
        }
    }
}

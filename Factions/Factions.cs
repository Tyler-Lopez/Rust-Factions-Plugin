// Requires: ZoneManager

using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("Factions", "Rust Factions", "1.0.0")]
    [Description("Pending Description")]
    public partial class Factions : RustPlugin
    {
        #region Global Variables
        private ZoneManagerApi _zoneManagerApi;
        #endregion

        // Todo, move to Oxide hooks?
        private void Loaded()
        {
            var manager = Manager;
            InitializeZoneManagerApi(manager);
        }

        private void InitializeZoneManagerApi(PluginManager manager)
        {
            _zoneManagerApi = ZoneManagerApi.CreateInstance(manager);
            if (_zoneManagerApi == null)
            {
                Puts("ZoneManager is not loaded! get it here https://umod.org/plugins/zone-manager");
            }
        }
    }
}﻿
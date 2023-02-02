// Requires: ZoneManager
// Requires: TruePVE

using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("Factions", "Rust Factions", "1.0.0")]
    [Description("Pending Description")]
    public partial class Factions : RustPlugin
    {
        #region Global Variables
        private ZoneManagerApi _truePveApi;
        private ZoneManagerApi _zoneManagerApi;
        #endregion

        private void Loaded()
        {
            var manager = Manager;
            InitializeTruePveApi(manager);
            InitializeZoneManagerApi(manager);
        }

        private void InitializeTruePveApi(PluginManager manager)
        {
            _zoneManagerApi = ZoneManagerApi.CreateInstance(manager);
            if (_zoneManagerApi == null)
            {
                Puts("TruePVE is not loaded! Get it here https://umod.org/plugins/true-pve");
            }
        }

        private void InitializeZoneManagerApi(PluginManager manager)
        {
            _zoneManagerApi = ZoneManagerApi.CreateInstance(manager);
            if (_zoneManagerApi == null)
            {
                Puts("ZoneManager is not loaded! Get it here https://umod.org/plugins/zone-manager");
            }
        }
    }
}﻿
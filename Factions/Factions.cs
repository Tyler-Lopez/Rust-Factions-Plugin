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
        private IZoneManagerRepository _zoneManagerRepository;
        #endregion

        private void Loaded()
        {
            var manager = Manager;
            InitializeZoneManagerApi(manager);
        }

        private void InitializeZoneManagerApi(PluginManager manager)
        {
            _zoneManagerRepository = ZoneManagerRepository.CreateInstance(manager);
            if (_zoneManagerRepository == null)
            {
                Puts("ZoneManager is not loaded! Get it here https://umod.org/plugins/zone-manager");
            }
        }
    }
}﻿
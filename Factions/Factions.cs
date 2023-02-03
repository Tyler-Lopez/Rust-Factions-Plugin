// Requires: ZoneManager

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
    }
}﻿
// Requires: ZoneManager

using Network;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("Factions", "Rust Factions", "1.0.0")]
    [Description("Pending Description")]
    public partial class Factions : RustPlugin
    {
        #region Global Variables
        private IZoneManagerRepository _zoneManagerRepository;

        public void Blah()
        {
            Network.Net.sv.Start();
            MapMarkerGenericRadius v;
            v.SendUpdate();
            SendInfo(BaseNetworkable.G)
        }
        #endregion
    }
}﻿
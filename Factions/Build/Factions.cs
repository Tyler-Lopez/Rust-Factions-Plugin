﻿// Requires: ZoneManager

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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using CompanionServer.Handlers;

namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Numerics;

    public partial class Factions
    {

        public class ZoneManagerApi
        {

            private readonly ZoneManager _zoneManager;

            /** Refer to Developer API https://umod.org/plugins/zone-manager **/
            private static class Constants
            {
                public const string ZoneManagerPluginName = "ZoneManager";

                public const string CreateOrUpdateZone = "CreateOrUpdateZone";
                public const string CreateOrUpdateZoneParameterName = "name";
                public const string CreateOrUpdateZoneParameterRadius = "radius";
            }

            public static ZoneManagerApi CreateInstance(PluginManager manager)
            {
                var zoneManager = manager.GetPlugin(Constants.ZoneManagerPluginName) as ZoneManager;
                return zoneManager == null ? null : new ZoneManagerApi(zoneManager);
            }

            private ZoneManagerApi(ZoneManager zoneManager)
            {
                _zoneManager = zoneManager;
            }

            public void HandlePlayerEnterZone(string zoneID, BasePlayer player)
            {
            }

            private bool CreateOrUpdateZone(string zoneId, string name, Vector3 location, int radius)
            {
                var argsMap = new Dictionary<string, string>
                {
                    [Constants.CreateOrUpdateZoneParameterName] = name,
                    [Constants.CreateOrUpdateZoneParameterRadius] = radius.ToString()
                };
                var response = _zoneManager.Call<bool?>(Constants.CreateOrUpdateZone, zoneId, argsMap.ToArray(), location);
                return response ?? false;
            }
        }
    }
}
﻿namespace Oxide.Plugins
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

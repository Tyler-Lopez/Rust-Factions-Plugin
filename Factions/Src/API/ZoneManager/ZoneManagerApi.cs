namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Numerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public partial class Factions
    {

        private class ZoneManagerApi
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

            public static Grid ParseZoneIdToGrid(string zoneId)
            {
                // Todo
                return null;
            }

            private ZoneManagerApi(ZoneManager zoneManager)
            {
                _zoneManager = zoneManager;
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

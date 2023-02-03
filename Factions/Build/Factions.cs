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
}﻿﻿namespace Oxide.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Oxide.Core.Plugins;

    partial class Factions
    {
        private class TruePveRepository : ITruePveRepository
        {
            bool ITruePveRepository.AddOrUpdateMapping(string key, string ruleset)
            {
                throw new System.NotImplementedException();
            }

            bool ITruePveRepository.RemoveMapping(string key)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
﻿namespace Oxide.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Oxide.Core.Plugins;

    partial class Factions
    {
        private class ZoneManagerRepository : IZoneManagerRepository 
        {
            private readonly ZoneManager _zoneManager;

            private static class Constants
            {
                public const string ZoneManagerPluginName = "ZoneManager";

                public const string CreateOrUpdateZone = "CreateOrUpdateZone";
                public const string CreateOrUpdateZoneParameterName = "name";
                public const string CreateOrUpdateZoneParameterRadius = "radius";
            }

            public static IZoneManagerRepository CreateInstance(PluginManager manager) {
                var zoneManager = manager.GetPlugin(Constants.ZoneManagerPluginName) as ZoneManager;
                return zoneManager == null ? null : new ZoneManagerRepository(zoneManager);
            }

            private ZoneManagerRepository(ZoneManager zoneManager)
            {
                _zoneManager = zoneManager;
            }

            bool IZoneManagerRepository.CreateOrUpdateZone(string zoneId, string name, UnityEngine.Vector3 location, int radius)
            {
                var argsMap = new Dictionary<string, string>
                {
                    [Constants.CreateOrUpdateZoneParameterName] = name,
                    [Constants.CreateOrUpdateZoneParameterRadius] = radius.ToString()
                };
                var response = _zoneManager.Call<bool?>(Constants.CreateOrUpdateZone, zoneId, argsMap.ToArray(), location);
                return response ?? false;
            }

            bool IZoneManagerRepository.EraseZone(string zoneId)
            {
                throw new System.NotImplementedException();
            }

            string[] IZoneManagerRepository.GetPlayerZoneIds(BasePlayer player)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
﻿namespace Oxide.Plugins
{
    using System.Text;
    public class Grid
    {
        private readonly byte _columnByte;
        private readonly string _columnString;
        private readonly byte _row;

        private static class Constants
        {
            public const char ColumnFirstChar = 'A';
            public const char ColumnLastBeforeRepeat = 'Z';
            public const char RowColumnDelimiter = ':';
        }

        public Grid(byte row, byte columnByte)
        {
            _row = row;
            _columnByte = columnByte;
            const int columnRange = (Constants.ColumnLastBeforeRepeat - Constants.ColumnFirstChar) + 1;
            var columnStringBuilder = new StringBuilder();
            for (var excessRanges = 0; excessRanges < (_columnByte / columnRange); excessRanges++)
            {
                columnStringBuilder.Append(Constants.ColumnFirstChar);
            }
            columnStringBuilder.Append((char)(Constants.ColumnFirstChar + (_columnByte % columnRange)));
            _columnString = columnStringBuilder.ToString();
        }

        public byte GetRow()
        {
            return _row;
        }

        public byte GetColumnNumeric()
        {
            return _columnByte;
        }

        public string GetColumnString()
        {
            return _columnString;
        }

        public override string ToString()
        {
            return $"{_columnString}{Constants.RowColumnDelimiter}{_row}";
        }
    }

}
﻿namespace Oxide.Plugins
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    public class Map : IEnumerable<Grid>
    {
        private int _columns;
        private int _rows;
        private readonly float _size;
        private List<Grid> _grids;

        public Map(int size)
        {
            _size = size;
        }

        private static class Constants
        {
            public const float GridCellSize = 146.3f;
        }

        public Vector2 GetGridCenter(Grid grid)
        {
            var centerOffset = Constants.GridCellSize / 2f;
            var halfWidth = Mathf.Floor((_rows * Constants.GridCellSize) / 2f);
            var halfHeight = Mathf.Floor((_rows * Constants.GridCellSize) / 2f);
            var offset = (_size - (_rows * Constants.GridCellSize)) / 2f;
            return new Vector2(
                (grid.GetColumnNumeric() * Constants.GridCellSize) - (halfWidth) - offset,
                 (grid.GetRow() * Constants.GridCellSize * -1) + (halfHeight - offset)
            );
        }

        public static float GetGridWidth()
        {
            return Constants.GridCellSize;
        }

        public static float GetGridHeight()
        {
            return Constants.GridCellSize;
        }

        public IEnumerator<Grid> GetEnumerator()
        {
            if (_grids != null)
            {
                foreach (var grid in _grids) yield return grid;
            }
            else
            {
                _grids = new List<Grid>();

                _columns = (int)Mathf.Floor(_size / Constants.GridCellSize);
                _rows = (int)Mathf.Floor(_size / Constants.GridCellSize);

                for (byte row = 0; row < _rows; row++)
                {
                    for (byte column = 0; column < _columns; column++)
                    {
                        _grids.Add(new Grid(row, column));
                        yield return _grids.Last();
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}

﻿namespace Oxide.Plugins
{
    partial class Factions
    {
        private interface ITruePveRepository
        {
            /// <summary>
            /// Adds or updates a mapping.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="ruleset"></param>
            /// <returns>True if successful. False if unsuccessful.</returns>
            bool AddOrUpdateMapping(string key, string ruleset);

            /// <summary>
            /// Removes a mapping.
            /// </summary>
            /// <param name="key"></param>
            /// <returns>True if successful. False if unsuccessful.</returns>
            bool RemoveMapping(string key);
        }
    }
}
﻿namespace Oxide.Plugins
{
    using UnityEngine;
    partial class Factions
    {
        /// <summary> Refer to Developer API https://umod.org/plugins/zone-manager </summary>
        private interface IZoneManagerRepository
        {
            /// <summary>
            /// Creates or updates a ZoneManager zone.
            /// </summary>
            /// <param name="zoneId"></param>
            /// <param name="name"></param>
            /// <param name="location"></param>
            /// <param name="radius"></param>
            /// <returns>Returns true if the zone is valid, else returns false if it was saved but not created (only reason would be that no position for the zone was set)</returns>
            bool CreateOrUpdateZone(string zoneId, string name, Vector3 location, int radius);

            /// <summary>
            /// Erase a zone by ZoneID or name.
            /// </summary>
            /// <param name="zoneId"></param>
            /// <returns>Returns true if the zone was deleted or false if the zone doesn't exist</returns>
            bool EraseZone(string zoneId);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="player"></param>
            /// <returns>Returns a string[] of IDs for zones the specified player is currently in, or null if none found</returns>
            string[] GetPlayerZoneIds(BasePlayer player);
        }
    }
}
﻿namespace Oxide.Plugins
{
    partial class Factions
    {
        public void InitializeMapForNewWipe()
        {
            Puts(ConVar.Server.worldsize.ToString());
            Puts(TerrainMeta.Size.ToString());
            var map = new Map(ConVar.Server.worldsize);
            /** For the map-size, generate **/
            foreach (var grid in map)
            {
                if (grid.GetRow() == 0)
                {
                    Puts(grid.ToString());
                    Puts(map.GetGridCenter(grid).ToString());
                }

            }
        }
    }
}
﻿namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Collections.Generic;
    using System.Text;
    using WebSocketSharp;
    partial class Factions
    {
        private void Loaded()
        {
            var manager = Manager;

            _zoneManagerRepository = ZoneManagerRepository.CreateInstance(manager);

            var missingPluginsConsoleMessage = new StringBuilder();

            if (_zoneManagerRepository == null)
            {
                missingPluginsConsoleMessage.AppendLine("ZoneManager is not loaded! Get it here https://umod.org/plugins/zone-manager.");
            }

            var message = missingPluginsConsoleMessage.ToString();
            if (!message.IsNullOrEmpty())
            {
                Puts(message);
            }

            InitializeMapForNewWipe();
        }
    }
}

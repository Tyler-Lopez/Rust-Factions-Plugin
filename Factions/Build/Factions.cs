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
    using Oxide.Core.Plugins;
    using System.Numerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
    public class Grids : IEnumerable<Grid>
    {
        private readonly float _width;
        private readonly float _height;
        private List<Grid> _grids;

        public Grids(float width, float height)
        {
            _width = width;
            _height = height;
        }

        private static class Constants
        {
            public const float GridCellSize = 146.3f;
        }

        public static Vector2 GetGridCenter(Grid grid)
        {
            return new Vector2(
                 grid.GetColumnNumeric() * Constants.GridCellSize,
                 grid.GetRow() * Constants.GridCellSize
            );
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

                var columns = Math.Round(_width / Constants.GridCellSize);
                var rows = Math.Round(_height / Constants.GridCellSize);

                for (byte row = 0; row < rows; row++)
                {
                    for (byte column = 0; column < columns; column++)
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
    using System;
    using UnityEngine;
    partial class Factions {
        public void InitializeMapForNewWipe()
        {
            var grids = new Grids(TerrainMeta.Size.x, TerrainMeta.Size.z);
            foreach (var grid in grids)
            {
                Puts(grid.ToString());
                Puts(Grids.GetGridCenter(grid).ToString());
            }
            /*
            MapSize = Mathf.Floor(.x / CellSize) * CellSize;
            MapWidth = Mathf.Floor(TerrainMeta.Size.x / CellSize) * CellSize;
            MapHeight = Mathf.Floor(TerrainMeta.Size.z / CellSize) * CellSize;


            NumberOfRows = (int)Math.Floor(MapHeight / (float)CellSize);
            NumberOfColumns = (int)Math.Floor(MapWidth / (float)CellSize);

            MapWidth = NumberOfColumns * CellSize;
            MapHeight = NumberOfRows * CellSize;

            MapOffsetX = TerrainMeta.Size.x - (NumberOfColumns * CellSize);
            MapOffsetZ = TerrainMeta.Size.z - (NumberOfRows * CellSize);
            RowIds = new string[NumberOfRows];
            ColumnIds = new string[NumberOfColumns];
            AreaIds = new string[NumberOfColumns, NumberOfRows];
            Positions = new Vector3[NumberOfColumns, NumberOfRows];
            Build();
            */
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

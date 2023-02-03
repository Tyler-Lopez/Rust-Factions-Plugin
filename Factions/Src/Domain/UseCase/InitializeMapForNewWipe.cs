namespace Oxide.Plugins
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

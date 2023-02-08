﻿namespace Oxide.Plugins
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    public sealed class Map : IEnumerable<Grid>
    {
        private readonly int _columns;
        private readonly int _rows;
        private readonly float _gridOffset;
        private List<Grid> _grids;

        public Map(int worldSize)
        {
            // The in-game map is always square
            _columns = (int)Mathf.Floor(worldSize / Constants.GridCellSize);
            _rows = _columns;
            // Sometimes (0,0,0) in-game is not the center of the center grid - the offset is how much it is off by
            var sizeUsedByGrids = _columns * Constants.GridCellSize;
            var sizeUsedByGridsHalved = sizeUsedByGrids / 2f;
            _gridOffset = (worldSize - sizeUsedByGridsHalved) / 2f;
        }

        private static class Constants
        {
            public const float GridCellSize = 146.3f;
        }

        public Vector2 GetGridTopLeft(Grid grid)
        {
            return new Vector2(
                (grid.GetColumnNumeric() * Constants.GridCellSize) - _gridOffset,
                (grid.GetRow() * Constants.GridCellSize * -1) + _gridOffset
            );
        }

        public Vector2 GetGridCenter(Grid grid)
        {
            var sizeOfGridHalved = Constants.GridCellSize / 2f;
            return new Vector2(
                (grid.GetColumnNumeric() * Constants.GridCellSize) - _gridOffset + sizeOfGridHalved,
                (grid.GetRow() * Constants.GridCellSize * -1) + _gridOffset - sizeOfGridHalved
            );
        }

        public static float GetGridSize()
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


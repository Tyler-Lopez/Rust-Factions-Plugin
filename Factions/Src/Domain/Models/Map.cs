namespace Oxide.Plugins
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


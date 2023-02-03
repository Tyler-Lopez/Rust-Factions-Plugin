namespace Oxide.Plugins
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


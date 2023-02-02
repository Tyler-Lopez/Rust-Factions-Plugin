namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Numerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Factions
    {
        private class Grid
        {
            public readonly char Row;
            public readonly byte Column;

            public Grid(char row, byte column)
            {
                Row = row;
                Column = column;
            }
        }
    }
}

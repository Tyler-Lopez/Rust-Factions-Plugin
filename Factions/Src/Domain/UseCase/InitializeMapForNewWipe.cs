namespace Oxide.Plugins
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

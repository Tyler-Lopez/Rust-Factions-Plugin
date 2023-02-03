namespace Oxide.Plugins
{
    partial class Factions
    {
        public void InitializeMapForNewWipe()
        {
            // For each grid on the map create a ZoneManager zone for them
            var map = new Map(ConVar.Server.worldsize);
            foreach (var grid in map)
            {
                _zoneManagerRepository.CreateZoneForGrid(
                    grid: grid,
                    center: map.GetGridCenter(grid),
                    gridSize: Map.GetGridSize()
                );
            }
        }
    }
}

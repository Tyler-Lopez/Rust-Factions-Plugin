namespace Oxide.Plugins
{
    partial class Factions
    {
        public void InitializeMapForNewWipe()
        {
            // For each factionsGrid on the map create a ZoneManager zone for them
            var map = new FactionsMap(ConVar.Server.worldsize);
            foreach (var grid in map)
            {
                _zoneManagerRepository.CreateZoneForGrid(
                    factionsGrid: grid,
                    center: map.GetGridCenter(grid),
                    gridSize: FactionsMap.GetGridSize()
                );
            }
        }
    }
}

namespace Oxide.Plugins
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
            _networkRepository = new NetworkRepository();

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

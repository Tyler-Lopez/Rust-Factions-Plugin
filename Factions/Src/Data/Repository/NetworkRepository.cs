namespace Oxide.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Oxide.Core.Plugins;
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    partial class Factions
    {
        private class NetworkRepository : INetworkRepository
        {
            public void SendMarkerToPlayer(BasePlayer player, MapMarkerGenericRadius marker)
            {
                throw new NotImplementedException();
            }
        }
    }
}

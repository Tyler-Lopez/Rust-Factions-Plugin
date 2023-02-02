namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Numerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public partial class Factions
    {

        private class TruePveApi
        {

            private readonly TruePVE _truePve;

            /** Refer to Developer API https://umod.org/plugins/true-pve **/
            private static class Constants
            {
                public const string TruePvePluginName = "TruePVE";
            }

            public static TruePveApi CreateInstance(PluginManager manager)
            {
                var truePve = manager.GetPlugin(Constants.TruePvePluginName) as TruePVE;
                return truePve == null ? null : new TruePveApi(truePve);
            }

            private TruePveApi(TruePVE truePve)
            {
                _truePve = truePve;
            }
        }
    }
}

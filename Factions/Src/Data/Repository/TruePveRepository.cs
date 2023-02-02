namespace Oxide.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Oxide.Core.Plugins;

    partial class Factions
    {
        private class TruePveRepository : ITruePveRepository
        {
            bool ITruePveRepository.AddOrUpdateMapping(string key, string ruleset)
            {
                throw new System.NotImplementedException();
            }

            bool ITruePveRepository.RemoveMapping(string key)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

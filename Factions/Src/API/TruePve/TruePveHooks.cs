namespace Oxide.Plugins
{
    using Oxide.Core.Plugins;
    using System.Numerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public partial class Factions
    {
        /** See External API Calls https://umod.org/plugins/true-pve **/
        private object CanEntityTakeDamage(BaseCombatEntity entity, HitInfo hitinfo)
        {
            return false;
        }
    }
}

namespace Oxide.Plugins
{
    partial class Factions
    {
        private interface ITruePveRepository
        {
            /// <summary>
            /// Adds or updates a mapping.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="ruleset"></param>
            /// <returns>True if successful. False if unsuccessful.</returns>
            bool AddOrUpdateMapping(string key, string ruleset);

            /// <summary>
            /// Removes a mapping.
            /// </summary>
            /// <param name="key"></param>
            /// <returns>True if successful. False if unsuccessful.</returns>
            bool RemoveMapping(string key);
        }
    }
}

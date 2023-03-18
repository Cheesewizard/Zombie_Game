using System.Collections.Generic;
using System.Reflection;
using Game.Configs;
using Sirenix.OdinInspector;

namespace Game.Utils
{
    public static partial class OdinDropdowns
    {
        public const string WEAPONS = "@OdinDropdowns.GetGunValueDropdowns()";

        private static List<ValueDropdownItem<int>> GetGunValueDropdowns()
        {
            var results = new ValueDropdownList<int>();

            var fields = typeof(WeaponIDs).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                results.Add(new ValueDropdownItem<int>(
                    $"{field.Name}",
                    (int)field.GetValue(null)));
            }

            return results;
        }
    }
}
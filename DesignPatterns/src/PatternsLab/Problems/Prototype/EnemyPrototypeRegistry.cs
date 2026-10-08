using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsLab.Problems.Prototype
{

    /// <summary>
    /// R5 Bonus: Stores named enemy prototypes and returns independent clones on demand.
    /// Tested in Program.cs.
    /// </summary>
    public class EnemyPrototypeRegistry
    {
        private readonly Dictionary<string, Enemy> _prototypes =
            new(StringComparer.OrdinalIgnoreCase);

        public void Register(string name, Enemy prototype)
        {
            _prototypes[name] = prototype;
        }

        public Enemy Create(string name)
        {
            if (!_prototypes.TryGetValue(name, out var prototype))
            {
                throw new KeyNotFoundException(
                    $"No enemy prototype registered with name '{name}'.");
            }

            return prototype.Clone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._6_KitchenTicket
{

    /// <summary>
    /// Detects food allergens from kitchen item ingredients.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when food allergen rules or ingredient mappings change.
    /// </remarks>
    public sealed class KitchenAllergenDetector
    {
        public IReadOnlyList<string> Detect(
            IEnumerable<IEnumerable<string>> ingredients)
        {
            var hits = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var itemIngredients in ingredients)
            {
                foreach (var ingredient in itemIngredients)
                {
                    var ing = ingredient.Trim().ToLowerInvariant();

                    if (ing.Contains("milk") ||
                        ing.Contains("cheese") ||
                        ing.Contains("butter"))
                    {
                        hits.Add("dairy");
                    }

                    if (ing.Contains("wheat") ||
                        ing.Contains("flour") ||
                        ing.Contains("bread"))
                    {
                        hits.Add("gluten");
                    }

                    if (ing.Contains("peanut") ||
                        ing.Contains("almond") ||
                        ing.Contains("cashew"))
                    {
                        hits.Add("nuts");
                    }

                    if (ing.Contains("shrimp") ||
                        ing.Contains("prawn") ||
                        ing.Contains("crab"))
                    {
                        hits.Add("shellfish");
                    }
                }
            }

            return hits.OrderBy(x => x).ToList();
        }
    }
}

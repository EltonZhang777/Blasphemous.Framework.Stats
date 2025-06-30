using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.Framework.Stats.Components;

internal class ItemCollection<T>
{
    internal IEnumerable<T> Items
    {
        get
        {
            return this.GetType()
                .GetFields()
                .Where(p => p.FieldType == typeof(T))
                .Select(p => (T)p.GetValue(this));
        }
    }
}

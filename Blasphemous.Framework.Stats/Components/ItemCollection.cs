using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;

internal class ItemCollection<T>
{
    internal IEnumerable<T> Items
    {
        get
        {
            return this.GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(T))
                .Select(p => (T)p.GetValue(this, null));
        }
    }
}

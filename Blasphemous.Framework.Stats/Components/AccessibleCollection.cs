using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;

internal class AccessibleCollection<TClass> : ItemCollection<IAccessible<TClass>>
{
    public void GetValueFrom(TClass classInstance)
    {
        this.Items.ToList().ForEach(x => x.GetValueFrom(classInstance));
    }

    public void SetValueTo(TClass classInstance)
    {
        this.Items.ToList().ForEach(x => x.SetValueTo(classInstance));
    }
}

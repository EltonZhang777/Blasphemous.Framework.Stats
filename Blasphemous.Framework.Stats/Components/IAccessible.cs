using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;

internal interface IAccessible<TClass>
{
    public abstract void GetValueFrom(TClass classInstance);
    public abstract void SetValueTo(TClass classInstance);
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;


internal class AccessibleProperty<TClass, TField>(string propertyName) : IAccessible<TClass>
{
    internal TField value;

    public void GetValueFrom(TClass classInstance)
    {
        value = Traverse.Create(classInstance).Property(propertyName).GetValue<TField>();
    }

    public void SetValueTo(TClass classInstance)
    {
        Traverse.Create(classInstance).Property(propertyName).SetValue(value);
    }
}

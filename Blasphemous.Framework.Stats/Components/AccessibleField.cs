using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;


internal class AccessibleField<TClass, TField>(string fieldName) : IAccessible<TClass>
{
    internal TField value;

    public void GetValueFrom(TClass classInstance)
    {
        value = Traverse.Create(classInstance).Field(fieldName).GetValue<TField>();
    }

    public void SetValueTo(TClass classInstance)
    {
        Traverse.Create(classInstance).Field(fieldName).SetValue(value);
    }
}

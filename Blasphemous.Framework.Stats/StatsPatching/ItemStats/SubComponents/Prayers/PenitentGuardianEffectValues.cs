using Blasphemous.Framework.Stats.Components;
using System;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR11: Tiento to your Thorned Hairs
/// </summary>
public class PenitentGuardianEffectValues : ObjectEffectValues, IAccessible_Class<PenitentGuardianEffect>
{
    public void GetValueFrom(PenitentGuardianEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PenitentGuardianEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}

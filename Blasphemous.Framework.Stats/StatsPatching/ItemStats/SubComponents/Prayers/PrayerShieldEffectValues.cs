using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR08: Zarabanda of the Safe Haven
/// </summary>
public class PrayerShieldEffectValues : ObjectEffect_StatValues, IAccessible_Class<PrayerShieldEffect>
{
    public void GetValueFrom(PrayerShieldEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PrayerShieldEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}

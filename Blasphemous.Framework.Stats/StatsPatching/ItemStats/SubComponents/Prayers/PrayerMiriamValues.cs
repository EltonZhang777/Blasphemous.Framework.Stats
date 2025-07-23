using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR201: Cantiña of the Blue Rose
/// </summary>
public class PrayerMiriamValues : ObjectEffectValues, IAccessible_Class<PrayerMiriam>
{
    public void GetValueFrom(PrayerMiriam obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PrayerMiriam obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}

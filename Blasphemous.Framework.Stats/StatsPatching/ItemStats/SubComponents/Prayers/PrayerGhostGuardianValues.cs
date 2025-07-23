using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR101: Aubade of the Nameless Guardian
/// </summary>
public class PrayerGhostGuardianValues : ObjectEffectValues, IAccessible_Class<PrayerGhostGuardian>
{
    public void GetValueFrom(PrayerGhostGuardian obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PrayerGhostGuardian obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}

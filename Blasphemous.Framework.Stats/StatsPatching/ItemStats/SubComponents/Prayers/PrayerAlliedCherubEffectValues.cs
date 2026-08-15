using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using System;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR05: Campanillero to the Sons of the Aurora
/// </summary>
public class PrayerAlliedCherubEffectValues : ObjectEffect_StatValues, IAccessible_Class<PrayerAlliedCherubEffect>
{
    public void GetValueFrom(PrayerAlliedCherubEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        throw new NotImplementedException();
    }

    public void SetValueTo(PrayerAlliedCherubEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        throw new NotImplementedException();
    }
}

using Blasphemous.Framework.Stats.Components;
using Framework.Inventory;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR101: Aubade of the Nameless Guardian
/// </summary>
public class PrayerGhostGuardianValues : ObjectEffectValues, IAccessible_Class<PrayerGhostGuardian>
{
    public void GetValueFrom(PrayerGhostGuardian obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

    }

    public void SetValueTo(PrayerGhostGuardian obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PrayerGhostGuardian t)
        {
            GetValueFrom(t);
        }
        else
        {
            base.GetValueFrom(obj);
        }
    }

    public override void SetValueTo(object obj)
    {
        if (obj is PrayerGhostGuardian t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}

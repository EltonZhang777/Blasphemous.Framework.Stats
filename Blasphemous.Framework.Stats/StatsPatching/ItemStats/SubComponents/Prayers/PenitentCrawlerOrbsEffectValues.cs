using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR14: Verdiales of the Forsaken Hamlet
/// </summary>
public class PenitentCrawlerOrbsEffectValues : ObjectEffectValues, IAccessible_Class<PenitentCrawlerOrbsEffect>
{
    public HitPatchData hitData;

    public void GetValueFrom(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        hitData = new()
        {
            basePrayerDamage = (float)Main.GetValue<int>(obj, "DamageAmount", Main.TraverseAccessType.Field),
            prayerBonusEfficiency = 1f
        };
    }

    public void SetValueTo(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "DamageAmount", (int?)hitData.basePrayerDamage, Main.TraverseAccessType.Field);

        // Register hit changes to patch controller
        hitData.CopyNonNullValuesTo(PatchController.Hits.PR14);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentCrawlerOrbsEffect t)
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
        if (obj is PenitentCrawlerOrbsEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}

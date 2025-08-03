using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR14: Verdiales of the Forsaken Hamlet
/// </summary>
public class PenitentCrawlerOrbsEffectValues : ObjectEffectValues, IAccessible_Class<PenitentCrawlerOrbsEffect>
{
    public float? baseDamage;
    public float? prayerBonusEfficiency = 1f;
    public HitValues hitValues;

    public void GetValueFrom(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        baseDamage = (float)Main.GetValue<int>(obj, "DamageAmount", Main.TraverseAccessType.Field);
    }

    public void SetValueTo(PenitentCrawlerOrbsEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        Main.SetValueIfNotNull(ref obj, "DamageAmount", (int?)baseDamage, Main.TraverseAccessType.Field);

        // Modify damage to corresponding HarmonyPatch
        PR14_HitPatch.hitValues = hitValues;
        PR14_HitPatch.baseDamage = baseDamage;
        PR14_HitPatch.prayerBonusEfficiency = prayerBonusEfficiency;
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

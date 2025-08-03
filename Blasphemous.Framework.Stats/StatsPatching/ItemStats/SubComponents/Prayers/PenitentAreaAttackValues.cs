using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Gameplay.GameControllers.Entities;
using HarmonyLib;
using Tools.Items;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR12: Cante Jondo of the Three Sisters
/// </summary>
public class PenitentAreaAttackValues : ObjectEffectValues, IAccessible_Class<PenitentAreaAttack>
{
    /// <summary>
    /// The max enemy detection range of Cante Jondo
    /// </summary>
    public float? attackRangeRadius;

    /// <summary>
    /// Delay between each hit and the next
    /// </summary>
    public float? delayBetweenHitsSeconds;

    /// <summary>
    /// The duration in which time is slowed during the casting animation
    /// </summary>
    public float? slowTimeDuration;

    public float? baseDamage;
    public float? prayerBonusEfficiency = 0.5f;

    public HitValues hitValues;

    public void GetValueFrom(PenitentAreaAttack obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        attackRangeRadius = Main.GetValue<float>(obj, "Radius", Main.TraverseAccessType.Field);
        delayBetweenHitsSeconds = Main.GetValue<float>(obj, "damageDelay", Main.TraverseAccessType.Field);
        slowTimeDuration = Main.GetValue<float>(obj, "slowTimeDuration", Main.TraverseAccessType.Field);
        baseDamage = Main.GetValue<float>(obj, "Amount", Main.TraverseAccessType.Field);

        // make the target create hit before getting the hit
        Traverse.Create(obj).Method("CreateHit").GetValue(null);
        hitValues = new();
        hitValues.GetValueFrom(Main.GetValue<Hit>(obj, "attackHit", Main.TraverseAccessType.Field));
    }

    public void SetValueTo(PenitentAreaAttack obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);
        Main.SetValueIfNotNull(ref obj, "Radius", attackRangeRadius, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "damageDelay", delayBetweenHitsSeconds, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "slowTimeDuration", slowTimeDuration, Main.TraverseAccessType.Field);

        // Modify damage to corresponding HarmonyPatch
        PR12_HitPatch.hitValues = hitValues;
        PR12_HitPatch.baseDamage = baseDamage;
        PR12_HitPatch.prayerBonusEfficiency = prayerBonusEfficiency;
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentAreaAttack t)
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
        if (obj is PenitentAreaAttack t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}

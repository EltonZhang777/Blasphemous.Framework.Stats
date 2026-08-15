using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches;
using Gameplay.GameControllers.Entities;
using HarmonyLib;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

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

    public HitPatchData hitData;

    public void GetValueFrom(PenitentAreaAttack obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        attackRangeRadius = TraverseUtils.GetValue<float>(obj, "Radius", TraverseUtils.TraverseAccessType.Field);
        delayBetweenHitsSeconds = TraverseUtils.GetValue<float>(obj, "damageDelay", TraverseUtils.TraverseAccessType.Field);
        slowTimeDuration = TraverseUtils.GetValue<float>(obj, "slowTimeDuration", TraverseUtils.TraverseAccessType.Field);

        hitData = new()
        {
            basePrayerDamage = TraverseUtils.GetValue<float>(obj, "Amount", TraverseUtils.TraverseAccessType.Field)
        };

        // make the target create hit before getting the hit
        Traverse.Create(obj).Method("CreateHit").GetValue(null);
        hitData.hitValues.GetValueFrom(TraverseUtils.GetValue<Hit>(obj, "attackHit", TraverseUtils.TraverseAccessType.Field));
    }

    public void SetValueTo(PenitentAreaAttack obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);
        TraverseUtils.SetValueIfNotNull(ref obj, "Radius", attackRangeRadius, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "damageDelay", delayBetweenHitsSeconds, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref obj, "slowTimeDuration", slowTimeDuration, TraverseUtils.TraverseAccessType.Field);

        // Register hit changes to patch controller
        hitData.CopyNonNullValuesTo(PatchController.Hits.PR12);
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

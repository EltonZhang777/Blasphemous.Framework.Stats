using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.Quirce.Attack;
using Gameplay.GameControllers.Penitent.Abilities;
using Tools.Items;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR07: Lorquiana
/// </summary>
public class PenitentMultishotEffectValues : ObjectEffectValues, IAccessible_Class<PenitentMultishotEffect>
{
    /// <summary>
    /// Total number of beams fired by the prayer
    /// </summary>
    public int? beamsCount;

    /// <summary>
    /// Delay between each beam shot in seconds
    /// </summary>
    public float? delayBetweenHitsSeconds;

    /// <summary>
    /// The duration in which time is slowed when a beam hits a target
    /// </summary>
    public float? slowTimeDuration;

    /// <summary>
    /// Sound played when a beam is shot
    /// </summary>
    public string beamShootSound;

    public HitPatchData hitData;

    public void GetValueFrom(PenitentMultishotEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        beamsCount = 3;
        delayBetweenHitsSeconds = 0.15f;
        hitData = new()
        {
            basePrayerDamage = TraverseUtils.GetValue<int>(obj, "DamageAmount", TraverseUtils.TraverseAccessType.Field),
            prayerBonusEfficiency = 0.35f
        };

        BossInstantProjectileAttack bossInstantProjectileAttack = Core.Logic.Penitent.GetComponentInChildren<PrayerUse>().multishotPrayer;
        slowTimeDuration = TraverseUtils.GetValue<float>(bossInstantProjectileAttack, "slowTimeDuration", TraverseUtils.TraverseAccessType.Field);
        beamShootSound = TraverseUtils.GetValue<string>(bossInstantProjectileAttack, "shotSound", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(PenitentMultishotEffect obj)
    {
        if (!TraverseUtils.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        PR07_HitPatch.beamsCount = beamsCount;
        PR07_HitPatch.delayBetweenHitsSeconds = delayBetweenHitsSeconds;
        hitData.CopyNonNullValuesTo(PatchController.Hits.PR07);

        PR07_HitPatch.slowTimeDuration = slowTimeDuration;
        PR07_HitPatch.beamShootSound = beamShootSound;
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentMultishotEffect t)
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
        if (obj is PenitentMultishotEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}

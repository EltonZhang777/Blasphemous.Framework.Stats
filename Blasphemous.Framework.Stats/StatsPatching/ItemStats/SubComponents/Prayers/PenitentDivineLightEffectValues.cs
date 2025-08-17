using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Patches;
using Blasphemous.Framework.Stats.Patches.ItemPatches;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.Quirce.Attack;
using Gameplay.GameControllers.Penitent.Abilities;
using HarmonyLib;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;

// WIP
/// <summary>
/// Effect for PR09: Taranto to My Sister
/// </summary>
public class PenitentDivineLightEffectValues : ObjectEffectValues, IAccessible_Class<PenitentDivineLightEffect>
{
    /// <summary>
    /// Total duration of the lightning storm, from start to finish
    /// </summary>
    public float? totalPrayerDuration; // seconds

    /// <summary>
    /// Scale multiplier for the x offset at which the lightning bolts will begin to be spawned from the penitent's position.
    /// </summary>
    public float? initialXOffsetScale; // offset

    public int? totalLightningBoltsCount; // totalAreas
    public float? distanceBetweenLightningBolts; // distanceBetweenAreas
    public HitPatchData hitData;

    /// <summary>
    /// Pool size of the lightning bolt object pool, automatically upscaled from the default 20 based on the total number of lightning bolts to be summoned.
    /// </summary>
    internal int LightningBoltPoolSize
    {
        get
        {
            if (!totalLightningBoltsCount.HasValue)
                return 20;

            return Mathf.Max(20, 8 + totalLightningBoltsCount.Value * 2);
        }
    }

    private BossAreaSummonAttack AreaSummonAttack => Core.Logic.Penitent.GetComponentInChildren<PrayerUse>().divineLightPrayer;
    private Traverse AreaSummonAttackTraverse => Traverse.Create(AreaSummonAttack);

    public void GetValueFrom(PenitentDivineLightEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.GetValueFrom(obj);

        totalPrayerDuration = Main.GetValue<float>(AreaSummonAttackTraverse, "seconds", Main.TraverseAccessType.Field);
        initialXOffsetScale = Main.GetValue<float>(AreaSummonAttackTraverse, "offset", Main.TraverseAccessType.Field);
        totalLightningBoltsCount = Main.GetValue<int>(AreaSummonAttackTraverse, "totalAreas", Main.TraverseAccessType.Field);
        distanceBetweenLightningBolts = Main.GetValue<float>(AreaSummonAttackTraverse, "distanceBetweenAreas", Main.TraverseAccessType.Field);

        hitData = new()
        {
            basePrayerDamage = (float)Main.GetValue<int>(AreaSummonAttackTraverse, "SpawnedAreaAttackDamage", Main.TraverseAccessType.Field),
            prayerBonusEfficiency = 1f
        };
    }

    public void SetValueTo(PenitentDivineLightEffect obj)
    {
        if (!Main.Validate(obj, x => x != null))
            return;

        base.SetValueTo(obj);

        // Cannot direction set the values to the AreaSummonAttack,
        // as it is a component of the Penitent's abilities,
        // which get refreshed each time a new Penitent instance is created.
        // Setting the values to the Harmony patch and patch them on-the-spot instead.
        PR09_HitPatch.totalPrayerDuration = totalPrayerDuration;
        PR09_HitPatch.initialXOffsetScale = initialXOffsetScale;
        PR09_HitPatch.totalLightningBoltsCount = totalLightningBoltsCount;
        PR09_HitPatch.distanceBetweenLightningBolts = distanceBetweenLightningBolts;
        PR09_HitPatch.lightningBoltPoolSize = LightningBoltPoolSize;

        // Register hit changes to patch controller
        hitData.CopyNonNullValuesTo(PatchController.Hits.PR09);
    }

    public override void GetValueFrom(object obj)
    {
        if (obj is PenitentDivineLightEffect t)
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
        if (obj is PenitentDivineLightEffect t)
        {
            SetValueTo(t);
        }
        else
        {
            base.SetValueTo(obj);
        }
    }
}

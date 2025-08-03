using Blasphemous.Framework.Stats.Extensions;
using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;
using Blasphemous.ModdingAPI;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.CommonAttacks;
using Gameplay.GameControllers.Enemies.BellGhost;
using Gameplay.GameControllers.Enemies.Projectiles;
using Gameplay.GameControllers.Entities;
using Gameplay.GameControllers.Penitent;
using Gameplay.GameControllers.Penitent.Abilities;
using HarmonyLib;
using System.Linq;
using Tools.Items;
using UnityEngine;

namespace Blasphemous.Framework.Stats.Patches.ItemPatches;

[HarmonyPatch(typeof(PenitentAreaAttack))]
class PR12_HitPatch
{
    [HarmonyPatch("CreateHit")]
    [HarmonyPostfix]
    public static void Postfix(ref Hit ___attackHit)
    {
        if (!IsActive)
            return;
#if DEBUG
        ModLog.Warn($"Modding PR12 hit!");
#endif

        hitValues?.SetValueTo(ref ___attackHit);
        if (baseDamage.HasValue && prayerBonusEfficiency.HasValue)
        {
            ___attackHit.DamageAmount = PatchController.CalculateFinalDamageWithBonuses(baseDamage.Value, PatchController.CalculatePrayerDamageMultiplier(prayerBonusEfficiency.Value));
        }
#if DEBUG
        ModLog.Warn($"PR12 hit final damage: `{___attackHit.DamageAmount}`!");
        ModLog.Warn($"PR12 hit element: `{___attackHit.DamageElement}`!");
#endif
    }

    public static float? baseDamage;
    public static float? prayerBonusEfficiency;
    public static HitValues hitValues;

    /// <summary>
    /// The patch should only be active if any active patch contains a modification to PR12
    /// </summary>
    public static bool IsActive => StatsPatchRegister.ItemPatches.Any(
        invItemStatsPatch =>
            invItemStatsPatch.isActive
            && invItemStatsPatch.statsPatches.Any(
                invItemData =>
                    invItemData.effectAdditions.Any(
                        objEffVal => objEffVal is PenitentAreaAttackValues)));
}


[HarmonyPatch(typeof(PenitentCrawlerOrbsEffect))]
class PR14_HitPatch
{
    [HarmonyPatch("OnApplyEffect")]
    [HarmonyPrefix]
    public static bool Prefix(
        PenitentCrawlerOrbsEffect __instance,
        Penitent ____owner,
        BossStraightProjectileAttack ____crawlerOrbs,
        bool __result)
    {
        if (!IsActive)
            return true;

        if (!baseDamage.HasValue || !prayerBonusEfficiency.HasValue)
            return true;


#if DEBUG
        ModLog.Warn($"Modding PR14 hit!");
#endif
        ____owner = Core.Logic.Penitent;
        ____crawlerOrbs = ____owner.GetComponentInChildren<PrayerUse>().crawlerBallsPrayer;
        Core.Logic.CameraManager.ProCamera2DShake.ShakeUsingPreset("SimpleHit");

        StraightProjectile straightProjectile;

        straightProjectile = ____crawlerOrbs.Shoot(Vector2.right, Vector2.right * 0.01f, PatchController.CalculatePrayerDamageMultiplier(prayerBonusEfficiency.Value));
        SetProjectileDamage(straightProjectile);

        straightProjectile = ____crawlerOrbs.Shoot(Vector2.left, Vector2.left * 0.01f, PatchController.CalculatePrayerDamageMultiplier(prayerBonusEfficiency.Value));
        SetProjectileDamage(straightProjectile);

#if DEBUG
        Hit displayHit = Traverse.Create(straightProjectile.GetComponent<ProjectileWeapon>()).Field("weaponHit").GetValue<Hit>();
        ModLog.Warn($"PR14 hit final damage: `{displayHit.DamageAmount}`!");
        ModLog.Warn($"PR14 hit element: `{displayHit.DamageElement}`!");
#endif

        __result = false;
        return false;

        void SetProjectileDamage(StraightProjectile proj)
        {
            ProjectileWeapon projectileWeapon = proj.GetComponent<ProjectileWeapon>();
            projectileWeapon.SetDamageAndCreateCustomHit(hitValues, baseDamage.Value, PatchController.CalculatePrayerDamageMultiplier(prayerBonusEfficiency.Value));
        }
    }

    public static float? baseDamage;
    public static float? prayerBonusEfficiency;
    public static HitValues hitValues;

    /// <summary>
    /// The patch should only be active if any active patch contains a modification to the target prayer
    /// </summary>
    public static bool IsActive => StatsPatchRegister.ItemPatches.Any(
        invItemStatsPatch =>
            invItemStatsPatch.isActive
            && invItemStatsPatch.statsPatches.Any(
                invItemData =>
                    invItemData.effectAdditions.Any(
                        objEffVal => objEffVal is PenitentCrawlerOrbsEffectValues)));
}
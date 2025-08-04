using Blasphemous.Framework.Stats.Extensions;
using Blasphemous.ModdingAPI;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.CommonAttacks;
using Gameplay.GameControllers.Enemies.BellGhost;
using Gameplay.GameControllers.Enemies.Projectiles;
using Gameplay.GameControllers.Entities;
using Gameplay.GameControllers.Penitent;
using Gameplay.GameControllers.Penitent.Abilities;
using HarmonyLib;
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
        if (!HitData.IsActive())
            return;
#if DEBUG
        ModLog.Warn($"Modding PR12 hit!");
#endif

        ___attackHit = HitData.CreateHit(___attackHit);
#if DEBUG
        ModLog.Warn($"PR12 hit final damage: `{___attackHit.DamageAmount}`!");
        ModLog.Warn($"PR12 hit element: `{___attackHit.DamageElement}`!");
#endif
    }

    public static HitPatchData HitData => PatchController.Hits.PR12;
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
        if (!HitData.IsActive())
            return true;

        if (!HitData.basePrayerDamage.HasValue || !HitData.prayerBonusEfficiency.HasValue)
            return true;


#if DEBUG
        ModLog.Warn($"Modding PR14 hit!");
#endif
        ____owner = Core.Logic.Penitent;
        ____crawlerOrbs = ____owner.GetComponentInChildren<PrayerUse>().crawlerBallsPrayer;
        Core.Logic.CameraManager.ProCamera2DShake.ShakeUsingPreset("SimpleHit");

        StraightProjectile straightProjectile;

        straightProjectile = ____crawlerOrbs.Shoot(Vector2.right, Vector2.right * 0.01f, 1);
        SetProjectileDamage(straightProjectile);

        straightProjectile = ____crawlerOrbs.Shoot(Vector2.left, Vector2.left * 0.01f, 1);
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
            projectileWeapon.CreateCustomHit(HitData.CreateHit());
        }
    }

    public static HitPatchData HitData => PatchController.Hits.PR14;
}
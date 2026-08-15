using Blasphemous.Framework.Stats.Extensions;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;
using Blasphemous.ModdingAPI;
using Framework.FrameworkCore;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.CommonAttacks;
using Gameplay.GameControllers.Bosses.Quirce.Attack;
using Gameplay.GameControllers.Enemies.BellGhost;
using Gameplay.GameControllers.Enemies.Projectiles;
using Gameplay.GameControllers.Entities;
using Gameplay.GameControllers.Penitent;
using Gameplay.GameControllers.Penitent.Abilities;
using HarmonyLib;
using System.Collections;
using Tools.Items;
using UnityEngine;
using Blasphemous.NewbieEltonLibs.Extensions.ModdingAPI;

namespace Blasphemous.Framework.Stats.Patches.ItemPatches;

/// <summary>
/// Patch for PR03: Debla of the Lights
/// </summary>
[HarmonyPatch(typeof(PenitentLightBeamEffect))]
class PR03_HitPatch
{
    [HarmonyPatch("OnApplyEffect")]
    [HarmonyPrefix]
    public static bool Prefix(
        PenitentLightBeamEffect __instance,
        bool __result,
        Penitent ____owner,
        BossAreaSummonAttack ____areaSummonAttack,
        Material ___oldMat)
    {
        if (!HitData.IsActive())
            return true;

        ModLogExtensions.WarnIfDebugBuild($"Modifying PR03 hit!");

        ____owner = Core.Logic.Penitent;
        ____areaSummonAttack = ____owner.GetComponentInChildren<PrayerUse>().lightBeamPrayer;

        // create new material for casting tint
        Material castingMaterial = new(penitentCastingTintMaterial)
        {
            color = penitentColorWhileCasting
        };
        __instance.penitentBlueTintMaterial = castingMaterial;

        Core.Logic.CameraManager.ProCamera2DShake.ShakeUsingPreset("SimpleHit");
        BossAreaSummonAttack areaSummonAttack = ____areaSummonAttack;
        Vector3 position = ____areaSummonAttack.transform.position;
        areaSummonAttack.InstantiateDeblaBeamWithCustomHit(HitData, areaSummonAttack.areaPrefab, position);
        __instance.StartCoroutine(ChangePenitentTintCoroutine());
        __result = false;
        return false;

        IEnumerator ChangePenitentTintCoroutine()
        {
            yield return new WaitForSeconds(0.4f);
            ___oldMat = ____owner.SpriteRenderer.material;
            ____owner.SpriteRenderer.material = __instance.penitentBlueTintMaterial;

            yield return new WaitForSeconds(0.8f);
            ____owner.SpriteRenderer.material = ___oldMat;
            yield break;
        }
    }

    public static void GetTintMaterial()
    {
        PenitentLightBeamEffect pr03 = Core.InventoryManager.GetInventoryItemFromId("PR03")
            .gameObject.GetComponent<PenitentLightBeamEffect>();
        penitentCastingTintMaterial = pr03.penitentBlueTintMaterial;
    }

    public static HitPatchData HitData => PatchController.Hits.PR03;
    public static Color penitentColorWhileCasting;
    public static Material penitentCastingTintMaterial;
}

/// <summary>
/// Patch for PR07: Lorquiana
/// </summary>
[HarmonyPatch(typeof(PenitentMultishotEffect))]
class PR07_HitPatch
{
    [HarmonyPatch("OnApplyEffect")]
    [HarmonyPrefix]
    public static bool Prefix(
        PenitentMultishotEffect __instance,
        Penitent ____owner,
        BossInstantProjectileAttack ____instantProjectileAttack,
        bool __result)
    {
        if (!HitData.IsActive())
            return true;

        ModLogExtensions.WarnIfDebugBuild($"Modifying PR07 hit!");
        __instance.StartCoroutine(LorquianaCoroutine());

        __result = false;
        return false;

        IEnumerator LorquianaCoroutine()
        {
            ____owner = Core.Logic.Penitent;
            ____instantProjectileAttack = ____owner.GetComponentInChildren<PrayerUse>().multishotPrayer;

            // modify slowTimeDuration and shotSound
            Main.SetValueIfNotNull(ref ____instantProjectileAttack, "slowTimeDuration", slowTimeDuration, Main.TraverseAccessType.Field);
            Main.SetValueIfNotNull(ref ____instantProjectileAttack, "shotSound", beamShootSound, Main.TraverseAccessType.Field);
            /*
            if (slowTimeDuration.HasValue)
                ____instantProjectileAttack.slowTimeDuration = slowTimeDuration.Value;
            if (!string.IsNullOrEmpty(beamShootSound))
                ____instantProjectileAttack.shotSound = beamShootSound;
            */

            // generate modded hit and apply it to the shots
            ____instantProjectileAttack.SetDamageStrength(1f);
            ____instantProjectileAttack.CreateLorquianaModdedHit(HitData);

            // initialize position
            Vector3 projectilePosition;
            Vector2 projectileDirection;
            InitializeProjectilePosition(____instantProjectileAttack, ____owner.Status.Orientation, out projectilePosition, out projectileDirection);

            // first beam shot
            ____instantProjectileAttack.Shoot(projectilePosition, projectileDirection);

            // repeat beam shots until count is reached
            for (int beamsFired = 1; beamsFired < BeamsCount; beamsFired++)
            {
                yield return new WaitForSeconds(DelayBetweenBeamsSeconds);
                InitializeProjectilePosition(____instantProjectileAttack, ____owner.Status.Orientation, out projectilePosition, out projectileDirection);
                ____instantProjectileAttack.Shoot(projectilePosition + (Vector3.up * GetRandomOffset(-1f, 1f)), projectileDirection);
            }

            yield break;
        }

        float GetRandomOffset(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        void InitializeProjectilePosition(
            BossInstantProjectileAttack bossInstantProjectileAttack,
            EntityOrientation orientation,
            out Vector3 position,
            out Vector2 direction)
        {
            direction = Main.EntityOrientationToDirectionalVector(____owner.Status.Orientation);
            bossInstantProjectileAttack.transform.localPosition = direction;
            position = bossInstantProjectileAttack.transform.position;
        }
    }

    public static HitPatchData HitData => PatchController.Hits.PR07;

    /// <summary>
    /// Total number of beams fired by the prayer
    /// </summary>
    public static int? beamsCount;

    /// <summary>
    /// Delay between each beam shot in seconds
    /// </summary>
    public static float? delayBetweenHitsSeconds;

    /// <summary>
    /// The duration in which time is slowed when a beam hits a target
    /// </summary>
    public static float? slowTimeDuration;

    /// <summary>
    /// Sound played when a beam is shot
    /// </summary>
    public static string beamShootSound;

    private static int BeamsCount => beamsCount.HasValue ? beamsCount.Value : 3;
    private static float DelayBetweenBeamsSeconds => delayBetweenHitsSeconds.HasValue ? delayBetweenHitsSeconds.Value : 0.15f;
}

/// <summary>
/// Patch for PR09: Taranto to My Sister
/// </summary>
[HarmonyPatch(typeof(PenitentDivineLightEffect))]
class PR09_HitPatch
{
    [HarmonyPatch("OnApplyEffect")]
    [HarmonyPrefix]
    public static bool Prefix(
        PenitentDivineLightEffect __instance,
        Penitent ____owner,
        BossAreaSummonAttack ____areaSummonAttack,
        bool __result)
    {
        if (!HitData.IsActive())
            return true;

        ModLogExtensions.WarnIfDebugBuild($"Modifying PR09 hit!");
        ____owner = Core.Logic.Penitent;
        ____areaSummonAttack = ____owner.GetComponentInChildren<PrayerUse>().divineLightPrayer;

        ____areaSummonAttack.SetDamageStrength(1);
        Main.SetValueIfNotNull(ref ____areaSummonAttack, "seconds", totalPrayerDuration, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref ____areaSummonAttack, "offset", initialXOffsetScale, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref ____areaSummonAttack, "totalAreas", totalLightningBoltsCount, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref ____areaSummonAttack, "distanceBetweenAreas", distanceBetweenLightningBolts, Main.TraverseAccessType.Field);
        Main.SetValue(ref ____areaSummonAttack, "poolSize", lightningBoltPoolSize, Main.TraverseAccessType.Field);

        ModLogExtensions.WarnIfDebugBuild($"total prayer duration when patching: {Traverse.Create(____areaSummonAttack).Field("seconds").GetValue<float>()}");
        ModLogExtensions.WarnIfDebugBuild($"total prayer duration when patching: {____areaSummonAttack.seconds}");
        // summon lightning
        ____areaSummonAttack.StartCoroutine(____areaSummonAttack.CustomHitLightningStormCoroutine(HitData, Core.Logic.Penitent.transform.position, Vector2.right));  //____areaSummonAttack.SummonAreas(Vector2.right);
        ____areaSummonAttack.StartCoroutine(____areaSummonAttack.CustomHitLightningStormCoroutine(HitData, Core.Logic.Penitent.transform.position, Vector2.left));  //____areaSummonAttack.SummonAreas(Vector2.left);

        Core.Logic.CameraManager.ProCamera2DShake.ShakeUsingPreset("SimpleHit");

        __result = false;
        return false;
    }

    public static HitPatchData HitData => PatchController.Hits.PR09;

    /// <summary>
    /// Total duration of the lightning storm, from start to finish
    /// </summary>
    public static float? totalPrayerDuration;

    /// <summary>
    /// Scale multiplier for the offset at which the lightning bolts will begin to be spawned from the penitent's position.
    /// </summary>
    public static float? initialXOffsetScale;

    public static int? totalLightningBoltsCount;
    public static float? distanceBetweenLightningBolts;
    public static int lightningBoltPoolSize;
}

/// <summary>
/// Patch for PR12: Cante Jondo of the Three Sisters
/// </summary>
[HarmonyPatch(typeof(PenitentAreaAttack))]
class PR12_HitPatch
{
    [HarmonyPatch("CreateHit")]
    [HarmonyPostfix]
    public static void Postfix(ref Hit ___attackHit)
    {
        if (!HitData.IsActive())
            return;
        ModLogExtensions.WarnIfDebugBuild($"Modding PR12 hit!");

        ___attackHit = HitData.CreateHit(___attackHit);
        ModLogExtensions.WarnIfDebugBuild($"PR12 hit final damage: `{___attackHit.DamageAmount}`!");
        ModLogExtensions.WarnIfDebugBuild($"PR12 hit element: `{___attackHit.DamageElement}`!");
    }

    public static HitPatchData HitData => PatchController.Hits.PR12;
}

/// <summary>
/// Patch for PR14: Verdiales of the Forsaken Hamlet
/// </summary>
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

        ModLogExtensions.WarnIfDebugBuild($"Modding PR14 hit!");
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

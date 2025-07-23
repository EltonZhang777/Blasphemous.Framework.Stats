using Blasphemous.ModdingAPI;
using Framework.Managers;
using Gameplay.GameControllers.Penitent.Attack;
using HarmonyLib;
using UnityEngine;

namespace Blasphemous.Framework.Stats.Patches.ItemPatches;

/// <summary>
/// Fixes ruby blade damage glitch
/// </summary>
[HarmonyPatch(typeof(CloisteredGemProjectileAttack))]
class CloisteredGemProjectileAttack_FixDamageStackBug_Patch
{
    /// <summary>
    /// Stores the base damage of the Cloistered Gem Bead blades when the projectile attack starts.
    /// </summary>
    [HarmonyPatch("OnStart")]
    [HarmonyPrefix]
    public static void Prefix1(CloisteredGemProjectileAttack __instance)
    {
        baseProjectileDamage = __instance.ProjectileDamageAmount;
#if DEBUG
        ModLog.Warn($"Storing cloistered gem blade base damage: {baseProjectileDamage}");
#endif
    }

    /// <summary>
    /// Reset base projectile damage from this patch to avoid the damage stack bug.
    /// </summary>
    [HarmonyPatch("OnUpdate")]
    [HarmonyPrefix]
    public static void Prefix2(CloisteredGemProjectileAttack __instance)
    {

        if (Mathf.Abs(Core.Logic.Penitent.Stats.Strength.Final - __instance.currentPenitentStregth) <= Mathf.Epsilon)
            return;

        __instance.ProjectileDamageAmount = baseProjectileDamage;
#if DEBUG
        ModLog.Warn($"Applying cloistered gem blade base damage: {baseProjectileDamage}");
#endif
    }

    public static int baseProjectileDamage;
}
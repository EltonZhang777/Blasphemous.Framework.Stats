/*
using Framework.FrameworkCore.Attributes;
using Gameplay.GameControllers.Entities;
using Gameplay.GameControllers.Penitent.Attack;
using HarmonyLib;

namespace Blasphemous.Framework.Stats.Patches;


// WIP: harmony patches that
// - unlimit capped values like attack speed
// - make some integer-rounded values (like penitent health) able to be float


internal class AttackSpeedUncap_Patches
{
    /// <summary>
    /// Uncap the attack speed at attribute level
    /// </summary>
    [HarmonyPatch(typeof(EntityStats), "Initialize")]
    [HarmonyPostfix]
    internal static void UncapStatsPostfix(EntityStats __instance, float ___AttackSpeedBase, float ___AttackSpeedUpgrade)
    {
        __instance.AttackSpeed = new AttackSpeed(___AttackSpeedBase, ___AttackSpeedUpgrade, float.MaxValue, 1f);
    }

    /// <summary>
    /// Uncap the attack speed setter for Penitent
    /// </summary>
    [HarmonyPatch(typeof(PenitentAttack), "AttackSpeed", MethodType.Setter)]
    [HarmonyPrefix]
    internal static bool UncapPenitentPostfix(ref float value, ref float ____attackSpeed, ref PenitentAttack __instance)
    {
        ____attackSpeed = value;
        PenitentSword penitentSword = (PenitentSword)__instance.CurrentPenitentWeapon;
        penitentSword.SlashAnimator.SetAnimatorSpeed(____attackSpeed);

        return false;
    }
}
*/
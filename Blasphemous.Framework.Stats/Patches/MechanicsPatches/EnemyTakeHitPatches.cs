using Gameplay.GameControllers.Enemies.Framework.Damage;
using Gameplay.GameControllers.Entities;
using HarmonyLib;

namespace Blasphemous.Framework.Stats.Patches.MechanicsPatches;

/// <summary>
/// Make EnemyDamageArea.TakeDamageAmount apply damage reduction of the enemy. 
/// (Yes, vanilla enemies don't use damage reduction even if it's non-zero)
/// </summary>
[HarmonyPatch(typeof(EnemyDamageArea), "TakeDamageAmount", [typeof(Hit)])]
class EnemyDamageArea_TakeDamageAmount_ApplyDamageReductionToEnemy_Patch
{
    [HarmonyPrefix]
    public static bool Prefix(
        Hit hit,
        Enemy ____enemyEntity)
    {
        if (!____enemyEntity)
        {
            return false;
        }

        float postMitigationDamage = ____enemyEntity.GetReducedDamage(hit);
        postMitigationDamage = !____enemyEntity.Status.Unattacable ? postMitigationDamage : 0;
        ____enemyEntity.Damage(postMitigationDamage, hit.HitSoundId);
        return false;
    }
}
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Gameplay.GameControllers.Enemies.BellGhost;
using Gameplay.GameControllers.Entities;
using HarmonyLib;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class ProjectileWeaponExtensions
{
    internal static void SetDamageAndCreateCustomHit(this ProjectileWeapon projectileWeapon, HitValues hitValues, float baseDamage, float finalMultiplier = 1f)
    {
        projectileWeapon.damage = (int)baseDamage;
        Traverse.Create(projectileWeapon).Field("_hitStrength").SetValue(finalMultiplier);

        Hit result = hitValues.CreateHitFromValues();
        result.DamageAmount = baseDamage * finalMultiplier;

        if (!projectileWeapon.AttackingEntity)
        {
            projectileWeapon.AttackingEntity = projectileWeapon.gameObject;
        }
        result.AttackingEntity = projectileWeapon.AttackingEntity;
        Traverse.Create(projectileWeapon).Field("weaponHit").SetValue(result);
    }

    internal static void CreateCustomHit(this ProjectileWeapon projectileWeapon, Hit hit)
    {
        if (!projectileWeapon.AttackingEntity)
        {
            projectileWeapon.AttackingEntity = projectileWeapon.gameObject;
        }
        hit.AttackingEntity = projectileWeapon.AttackingEntity;

        Traverse.Create(projectileWeapon).Field("weaponHit").SetValue(hit);
    }
}

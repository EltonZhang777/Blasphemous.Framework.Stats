using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Blasphemous.ModdingAPI;
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
#if DEBUG
        ModLog.Info($"hit element in weaponHit: {Traverse.Create(projectileWeapon).Field("weaponHit").GetValue<Hit>().DamageElement}");
#endif
    }
}

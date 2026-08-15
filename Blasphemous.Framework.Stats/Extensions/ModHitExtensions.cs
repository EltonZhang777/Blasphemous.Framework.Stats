using Blasphemous.Framework.Stats.Patches;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Framework.FrameworkCore;
using Framework.Managers;
using Gameplay.GameControllers.Bosses.Quirce.Attack;
using Gameplay.GameControllers.Enemies.BellGhost;
using Gameplay.GameControllers.Entities;
using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class ModHitExtensions
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

    /// <summary>
    /// For PR09: Taranto to My Sister
    /// </summary>
    internal static IEnumerator CustomHitLightningStormCoroutine(
        this BossAreaSummonAttack bossAreaSummonAttack,
        HitPatchData hitData,
        Vector3 originPosition,
        Vector3 direction,
        EntityOrientation orientation = EntityOrientation.Right)
    {
        float counter = 0f;
        int areasSummoned = 0;
        Vector3 lastPoint = originPosition + (direction * bossAreaSummonAttack.offset);
        bool cancelled = false;
        int currentTotalAreas = bossAreaSummonAttack.totalAreas;
        float currentDistanceBetweenAreas = bossAreaSummonAttack.distanceBetweenAreas;
        GetTraverse().Field("lastRandomAreaIndex").SetValue(-1);  // this.lastRandomAreaIndex = -1;
        while (counter < bossAreaSummonAttack.seconds)
        {
            float normalizedValue = bossAreaSummonAttack.curve.Evaluate(counter / bossAreaSummonAttack.seconds);
            if ((float)areasSummoned / (float)currentTotalAreas <= normalizedValue)
            {
                if (bossAreaSummonAttack.checkCollisions || cancelled)
                {
                    RaycastHit2D[] array = new RaycastHit2D[1];
                    bool flag = Physics2D.LinecastNonAlloc(originPosition, lastPoint, array, bossAreaSummonAttack.collisionMask) > 0;
                    if (flag)
                    {
                        Debug.DrawLine(array[0].point, array[0].point + (Vector2.up * 0.25f), Color.red, 1f);
                        cancelled = true;
                    }
                }
                if (!cancelled)
                {
                    GameObject gameObject;

                    // injecting modded hit here
                    gameObject = bossAreaSummonAttack.InstantiateLightningBoltWithCustomHit(hitData, bossAreaSummonAttack.areaPrefab, lastPoint, 0f);  //gameObject = this.InstantiateArea(this.areaPrefab, lastPoint, 0f, this.damageMultiplier, null);
                    Entity component2 = gameObject.GetComponent<Entity>();
                    component2?.SetOrientation(orientation, true, false);
                    areasSummoned++;
                }
                lastPoint += direction * currentDistanceBetweenAreas;
            }
            yield return null;
            counter += Time.deltaTime;
        }
        yield break;

        Traverse GetTraverse()
        {
            return Traverse.Create(bossAreaSummonAttack);
        }
    }

    /// <summary>
    /// For PR09: Taranto to My Sister
    /// </summary>
    internal static GameObject InstantiateLightningBoltWithCustomHit(
        this BossAreaSummonAttack bossAreaSummonAttack,
        HitPatchData hitData,
        GameObject toInstantiate,
        Vector3 point,
        float angle = 0f)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject gameObject = PoolManager.Instance.ReuseObject(toInstantiate, point, rotation, false, 1).GameObject;
        BossSpawnedAreaAttack component = gameObject.GetComponent<BossSpawnedAreaAttack>();
        if (component != null)
        {
            component.SetOwner(bossAreaSummonAttack.EntityOwner);
            component.SetDamageStrength(1f);

            // create modded hit
            component.CreateHit();
            Hit vanillaHit = Traverse.Create(component).Field("_hit").GetValue<Hit>();
            Hit moddedHit = hitData.CreateHit(vanillaHit);
            moddedHit.AttackingEntity = vanillaHit.AttackingEntity;
            Traverse.Create(component).Field("_hit").SetValue(moddedHit);
        }
        if (bossAreaSummonAttack.instantiations == null)
        {
            bossAreaSummonAttack.instantiations = [];
        }
        if (!bossAreaSummonAttack.instantiations.Contains(gameObject))
        {
            bossAreaSummonAttack.instantiations.Add(gameObject);
        }
        return gameObject;
    }

    /// <summary>
    /// For PR03: Debla of the Lights
    /// </summary>
    internal static GameObject InstantiateDeblaBeamWithCustomHit(
        this BossAreaSummonAttack bossAreaSummonAttack,
        HitPatchData hitData,
        GameObject toInstantiate,
        Vector3 point,
        float angle = 0f)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject gameObject = PoolManager.Instance.ReuseObject(toInstantiate, point, rotation, false, 1).GameObject;
        BossSpawnedAreaAttack component = gameObject.GetComponent<BossSpawnedAreaAttack>();
        if (component != null)
        {
            component.SetOwner(bossAreaSummonAttack.EntityOwner);
            component.SetDamageStrength(1f);

            // create modded hit
            component.CreateHit();
            Hit vanillaHit = Traverse.Create(component).Field("_hit").GetValue<Hit>();
            Hit moddedHit = hitData.CreateHit(vanillaHit);
            moddedHit.AttackingEntity = vanillaHit.AttackingEntity;
            Traverse.Create(component).Field("_hit").SetValue(moddedHit);
        }
        if (bossAreaSummonAttack.instantiations == null)
        {
            bossAreaSummonAttack.instantiations = [];
        }
        if (!bossAreaSummonAttack.instantiations.Contains(gameObject))
        {
            bossAreaSummonAttack.instantiations.Add(gameObject);
        }
        return gameObject;
    }

    internal static void CreateLorquianaModdedHit(
        this BossInstantProjectileAttack bossInstantProjectileAttack,
        HitPatchData hitData)
    {
        bossInstantProjectileAttack.CreateHit();
        Hit vanillaHit = Traverse.Create(bossInstantProjectileAttack).Field("_hit").GetValue<Hit>();
        Hit moddedHit = hitData.CreateHit(vanillaHit);
        moddedHit.AttackingEntity = vanillaHit.AttackingEntity;
        Traverse.Create(bossInstantProjectileAttack).Field("_hit").SetValue(moddedHit);
    }
}

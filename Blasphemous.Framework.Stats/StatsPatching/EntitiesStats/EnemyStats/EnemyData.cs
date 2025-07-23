using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Enemies.Framework.Attack;
using Gameplay.GameControllers.Entities;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.EnemyStats;

/// <summary>
/// Stats of a type of Enemy
/// </summary>
public class EnemyData : IAccessible_Class<Enemy>, IStatsPatchable
{
    [JsonProperty(Required = Required.Always)]
    public string entityId;

    public EntityStatsValues_Enemy entityStats = new();
    public PlatformCharacterControllerValues_Enemy platformCharacterController = new();
    public float? tearsDrop;
    public float? contactDamage;

    [JsonIgnore] protected List<Enemy> targetEnemies;

    /// <inheritdoc/>
    public void GetValueFrom(Enemy obj)
    {
        entityStats.GetValueFrom(obj.Stats);

        PlatformCharacterController pcc = GetPcc(obj);
        if (pcc != null)
        {
            platformCharacterController.GetValueFrom(pcc);
        }

        tearsDrop = obj.purgePointsWhenDead;

        EnemyAttack enemyAttack = obj.GetComponentInChildren<EnemyAttack>();
        if (enemyAttack != null)
        {
            contactDamage = enemyAttack.ContactDamageAmount;
        }
    }

    /// <inheritdoc/>
    public void SetValueTo(Enemy obj)
    {
        entityStats.SetValueTo(obj.Stats);

        PlatformCharacterController pcc = GetPcc(obj);
        if (pcc != null)
        {
            platformCharacterController.SetValueTo(pcc);
        }

        if (tearsDrop.HasValue)
        {
            obj.purgePointsWhenDead = tearsDrop.Value;
        }

        EnemyAttack enemyAttack = obj.GetComponentInChildren<EnemyAttack>();
        if (enemyAttack != null && contactDamage.HasValue)
        {
            enemyAttack.ContactDamageAmount = contactDamage.Value;
        }
    }

    /// <inheritdoc/>
    public bool TryGetTargets()
    {
        targetEnemies = UObject.FindObjectsOfType<Enemy>().Where(x => x.Id == entityId).ToList();
        return targetEnemies.Count > 0;
    }

    /// <summary>
    /// Get value from first matching enemy of the same `entityId`
    /// </summary>
    public void GetValueFromFirstTarget()
    {
        if (TryGetTargets())
        {
            GetValueFrom(targetEnemies.First());
        }
    }

    /// <summary>
    /// Set value to all available enemies of the same `entityId`
    /// </summary>
    public void SetValueToAllTargets()
    {
        if (!TryGetTargets())
            return;

        foreach (Enemy enemy in targetEnemies)
        {
            SetValueTo(enemy);

            // set patched enemies' life to current max
            enemy.Stats.Life.SetToCurrentMax();

            // re-initialize EnemyAttack to apply Strength's value to the attack.
            EnemyAttack enemyAttack = enemy.GetComponentInChildren<EnemyAttack>();
            Traverse.Create(enemyAttack).Method("OnStart").GetValue(new object[] { });
        }
    }

    private PlatformCharacterController GetPcc(Enemy enemy)
    {
        return enemy.gameObject.GetComponent<PlatformCharacterController>();
    }
}

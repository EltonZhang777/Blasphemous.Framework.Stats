using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Extensions;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;
using Gameplay.GameControllers.Penitent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Blasphemous.Framework.Stats.EnemiesInfo;

/// <summary>
/// Stats of a type of Enemy
/// </summary>
public class EnemyData : IAccessible<Enemy>
{
    [JsonProperty] public EntityStatsValues_Enemy entityStats = new();
    [JsonProperty] public PlatformCharacterControllerValues_Enemy platformCharacterController = new();

    /// <inheritdoc/>
    public void GetValueFrom(Enemy obj)
    {
        entityStats.GetValueFrom(obj.Stats);
        PlatformCharacterController pcc = GetPcc(obj);
        if (pcc != null)
        {
            platformCharacterController.GetValueFrom(pcc);
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
    }

    private PlatformCharacterController GetPcc(Enemy enemy)
    {
        return enemy.gameObject.GetComponent<PlatformCharacterController>();
    }
}

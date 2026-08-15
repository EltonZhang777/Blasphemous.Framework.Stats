using Blasphemous.Framework.Stats.Components;
using Gameplay.GameControllers.Entities;
using Newtonsoft.Json;
using UnityEngine;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

public class HitValues : IAccessible_Struct<Hit>
{
    [JsonIgnore]
    public GameObject attackingEntity;

    // damage properties
    public float? damageAmount;
    public DamageArea.DamageType? damageType;
    public DamageArea.DamageElement? damageElement;

    // avoidability
    public bool? unavoidable;
    public bool? unparriable;
    public bool? unblockable;

    // knockback properties
    public float? knockbackDistance;
    public bool? knockbackDirectionByOwnerPosition;
    public bool? forceGuardslide;
    public bool? forceGuardSlideDirection;
    public bool? checkOrientationsForGuardslide;

    // misc. properties
    public bool? destroysProjectiles;
    public bool? dontSpawnBlood;

    // hit sound
    public string hitSoundId;

    public void GetValueFrom(Hit obj)
    {
        attackingEntity = TraverseUtils.GetValue<GameObject>(obj, "AttackingEntity", TraverseUtils.TraverseAccessType.Field);

        damageAmount = TraverseUtils.GetValue<float>(obj, "DamageAmount", TraverseUtils.TraverseAccessType.Field);
        damageType = TraverseUtils.GetValue<DamageArea.DamageType>(obj, "DamageType", TraverseUtils.TraverseAccessType.Field);
        damageElement = TraverseUtils.GetValue<DamageArea.DamageElement>(obj, "DamageElement", TraverseUtils.TraverseAccessType.Field);

        unavoidable = TraverseUtils.GetValue<bool>(obj, "Unnavoidable", TraverseUtils.TraverseAccessType.Field);
        unparriable = TraverseUtils.GetValue<bool>(obj, "Unparriable", TraverseUtils.TraverseAccessType.Field);
        unblockable = TraverseUtils.GetValue<bool>(obj, "Unblockable", TraverseUtils.TraverseAccessType.Field);

        knockbackDistance = TraverseUtils.GetValue<float>(obj, "Force", TraverseUtils.TraverseAccessType.Field);
        knockbackDirectionByOwnerPosition = TraverseUtils.GetValue<bool>(obj, "ThrowbackDirByOwnerPosition", TraverseUtils.TraverseAccessType.Field);
        forceGuardslide = TraverseUtils.GetValue<bool>(obj, "forceGuardslide", TraverseUtils.TraverseAccessType.Field);
        forceGuardSlideDirection = TraverseUtils.GetValue<bool>(obj, "ForceGuardSlideDirection", TraverseUtils.TraverseAccessType.Field);
        checkOrientationsForGuardslide = TraverseUtils.GetValue<bool>(obj, "CheckOrientationsForGuardslide", TraverseUtils.TraverseAccessType.Field);

        destroysProjectiles = TraverseUtils.GetValue<bool>(obj, "DestroysProjectiles", TraverseUtils.TraverseAccessType.Field);
        dontSpawnBlood = TraverseUtils.GetValue<bool>(obj, "DontSpawnBlood", TraverseUtils.TraverseAccessType.Field);

        hitSoundId = TraverseUtils.GetValue<string>(obj, "HitSoundId", TraverseUtils.TraverseAccessType.Field);
    }

    public void SetValueTo(ref Hit obj)
    {
        if (attackingEntity != null)
            obj.AttackingEntity = attackingEntity;

        if (damageAmount.HasValue)
            obj.DamageAmount = damageAmount.Value;
        if (damageType.HasValue)
            obj.DamageType = damageType.Value;
        if (damageElement.HasValue)
            obj.DamageElement = damageElement.Value;

        if (unavoidable.HasValue)
            obj.Unnavoidable = unavoidable.Value;
        if (unparriable.HasValue)
            obj.Unparriable = unparriable.Value;
        if (unblockable.HasValue)
            obj.Unblockable = unblockable.Value;

        if (knockbackDistance.HasValue)
            obj.Force = knockbackDistance.Value;
        if (knockbackDirectionByOwnerPosition.HasValue)
            obj.ThrowbackDirByOwnerPosition = knockbackDirectionByOwnerPosition.Value;
        if (forceGuardslide.HasValue)
            obj.forceGuardslide = forceGuardslide.Value;
        if (forceGuardSlideDirection.HasValue)
            obj.ForceGuardSlideDirection = forceGuardSlideDirection.Value;
        if (checkOrientationsForGuardslide.HasValue)
            obj.CheckOrientationsForGuardslide = checkOrientationsForGuardslide.Value;

        if (destroysProjectiles.HasValue)
            obj.DestroysProjectiles = destroysProjectiles.Value;
        if (dontSpawnBlood.HasValue)
            obj.DontSpawnBlood = dontSpawnBlood.Value;

        if (hitSoundId != null)
            obj.HitSoundId = hitSoundId;
    }

    public Hit CreateHitFromValues()
    {
        Hit result = new();
        SetValueTo(ref result);
        return result;
    }
}

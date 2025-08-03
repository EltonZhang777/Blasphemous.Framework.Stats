using Blasphemous.Framework.Stats.Components;
using Gameplay.GameControllers.Entities;
using Newtonsoft.Json;
using UnityEngine;

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
        attackingEntity = Main.GetValue<GameObject>(obj, "AttackingEntity", Main.TraverseAccessType.Field);

        damageAmount = Main.GetValue<float>(obj, "DamageAmount", Main.TraverseAccessType.Field);
        damageType = Main.GetValue<DamageArea.DamageType>(obj, "DamageType", Main.TraverseAccessType.Field);
        damageElement = Main.GetValue<DamageArea.DamageElement>(obj, "DamageElement", Main.TraverseAccessType.Field);

        unavoidable = Main.GetValue<bool>(obj, "Unnavoidable", Main.TraverseAccessType.Field);
        unparriable = Main.GetValue<bool>(obj, "Unparriable", Main.TraverseAccessType.Field);
        unblockable = Main.GetValue<bool>(obj, "Unblockable", Main.TraverseAccessType.Field);

        knockbackDistance = Main.GetValue<float>(obj, "Force", Main.TraverseAccessType.Field);
        knockbackDirectionByOwnerPosition = Main.GetValue<bool>(obj, "ThrowbackDirByOwnerPosition", Main.TraverseAccessType.Field);
        forceGuardslide = Main.GetValue<bool>(obj, "forceGuardslide", Main.TraverseAccessType.Field);
        forceGuardSlideDirection = Main.GetValue<bool>(obj, "ForceGuardSlideDirection", Main.TraverseAccessType.Field);
        checkOrientationsForGuardslide = Main.GetValue<bool>(obj, "CheckOrientationsForGuardslide", Main.TraverseAccessType.Field);

        destroysProjectiles = Main.GetValue<bool>(obj, "DestroysProjectiles", Main.TraverseAccessType.Field);
        dontSpawnBlood = Main.GetValue<bool>(obj, "DontSpawnBlood", Main.TraverseAccessType.Field);

        hitSoundId = Main.GetValue<string>(obj, "HitSoundId", Main.TraverseAccessType.Field);
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

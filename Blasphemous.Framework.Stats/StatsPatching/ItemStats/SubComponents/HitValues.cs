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

    /*
    public string HitSoundId;
    public GameObject AttackingEntity;
    public DamageArea.DamageType DamageType;
    public DamageArea.DamageElement DamageElement;
    public bool Unnavoidable;
    public bool ForceGuardSlideDirection;
    public bool Unparriable;
    public bool Unblockable;
    public bool DestroysProjectiles;
    public bool DontSpawnBlood;
    public bool forceGuardslide;
    public bool CheckOrientationsForGuardslide;
    public bool ThrowbackDirByOwnerPosition;
    public float DamageAmount;
    public float Force;
    */

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
        Main.SetValueIfNotNull(ref obj, "AttackingEntity", attackingEntity, Main.TraverseAccessType.Field);

        Main.SetValueIfNotNull(ref obj, "DamageAmount", damageAmount, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "DamageType", damageType, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "DamageElement", damageElement, Main.TraverseAccessType.Field);

        Main.SetValueIfNotNull(ref obj, "Unnavoidable", unavoidable, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "Unparriable", unparriable, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "Unblockable", unblockable, Main.TraverseAccessType.Field);

        Main.SetValueIfNotNull(ref obj, "Force", knockbackDistance, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "ThrowbackDirByOwnerPosition", knockbackDirectionByOwnerPosition, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "forceGuardslide", forceGuardslide, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "ForceGuardSlideDirection", forceGuardSlideDirection, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "CheckOrientationsForGuardslide", checkOrientationsForGuardslide, Main.TraverseAccessType.Field);

        Main.SetValueIfNotNull(ref obj, "DestroysProjectiles", destroysProjectiles, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref obj, "DontSpawnBlood", dontSpawnBlood, Main.TraverseAccessType.Field);

        Main.SetValueIfNotNull(ref obj, "HitSoundId", hitSoundId, Main.TraverseAccessType.Field);
    }
}

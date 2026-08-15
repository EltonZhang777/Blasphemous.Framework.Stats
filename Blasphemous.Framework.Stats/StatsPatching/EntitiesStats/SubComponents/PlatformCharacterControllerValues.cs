using Blasphemous.Framework.Stats.Components;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;
using Blasphemous.NewbieEltonLibs.Extensions.GameLibs;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class PlatformCharacterControllerValues_Penitent : PlatformCharacterControllerValues_Enemy, IAccessible_Class<PlatformCharacterController>
{
    public float? platformDropTime;
    public float? ghostJumpDelay;
    public float? ladderJumpTimeThreshold;
    public float? climbingSpeed;

    /// <inheritdoc/>
    public override void GetValueFrom(PlatformCharacterController pcc)
    {
        base.GetValueFrom(pcc);

        if (!TraverseUtils.Validate(pcc, x => x != null))
            return;

        platformDropTime = TraverseUtils.GetValue<float>(pcc, "PlatformDropTime", TraverseUtils.TraverseAccessType.Property);
        ghostJumpDelay = TraverseUtils.GetValue<float>(pcc, "GhostJumpDelay", TraverseUtils.TraverseAccessType.Property);
        ladderJumpTimeThreshold = TraverseUtils.GetValue<float>(pcc, "m_ladderJumpTimeThreshold", TraverseUtils.TraverseAccessType.Field);
        climbingSpeed = TraverseUtils.GetValue<float>(pcc, "ClimbingSpeed", TraverseUtils.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public override void SetValueTo(PlatformCharacterController pcc)
    {
        base.SetValueTo(pcc);

        if (!TraverseUtils.Validate(pcc, x => x != null))
            return;

        TraverseUtils.SetValueIfNotNull(ref pcc, "PlatformDropTime", platformDropTime, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "GhostJumpDelay", ghostJumpDelay, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "m_ladderJumpTimeThreshold", ladderJumpTimeThreshold, TraverseUtils.TraverseAccessType.Field);
        TraverseUtils.SetValueIfNotNull(ref pcc, "ClimbingSpeed", climbingSpeed, TraverseUtils.TraverseAccessType.Property);
    }
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Enemy"/>
/// </summary>
public class PlatformCharacterControllerValues_Enemy : IAccessible_Class<PlatformCharacterController>
{
    public float? walkingAcceleration;
    public float? walkingDrag;
    public float? maxWalkingSpeed;
    public float? airborneAcceleration;
    public float? jumpingSpeed;
    public float? jumpingAcceleration;
    public float? jumpingAccelerationTime;

    /// <inheritdoc/>
    public virtual void GetValueFrom(PlatformCharacterController pcc)
    {
        if (!TraverseUtils.Validate(pcc, x => x != null))
            return;

        walkingAcceleration = TraverseUtils.GetValue<float>(pcc, "WalkingAcc", TraverseUtils.TraverseAccessType.Property);
        walkingDrag = TraverseUtils.GetValue<float>(pcc, "WalkingDrag", TraverseUtils.TraverseAccessType.Property);
        maxWalkingSpeed = TraverseUtils.GetValue<float>(pcc, "MaxWalkingSpeed", TraverseUtils.TraverseAccessType.Property);
        airborneAcceleration = TraverseUtils.GetValue<float>(pcc, "AirborneAcc", TraverseUtils.TraverseAccessType.Property);
        jumpingSpeed = TraverseUtils.GetValue<float>(pcc, "JumpingSpeed", TraverseUtils.TraverseAccessType.Property);
        jumpingAcceleration = TraverseUtils.GetValue<float>(pcc, "JumpingAcc", TraverseUtils.TraverseAccessType.Property);
        jumpingAccelerationTime = TraverseUtils.GetValue<float>(pcc, "JumpingAccTime", TraverseUtils.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public virtual void SetValueTo(PlatformCharacterController pcc)
    {
        if (!TraverseUtils.Validate(pcc, x => x != null))
            return;

        TraverseUtils.SetValueIfNotNull(ref pcc, "WalkingAcc", walkingAcceleration, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "WalkingDrag", walkingDrag, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "AirborneAcc", airborneAcceleration, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "JumpingSpeed", jumpingSpeed, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "JumpingAcc", jumpingAcceleration, TraverseUtils.TraverseAccessType.Property);
        TraverseUtils.SetValueIfNotNull(ref pcc, "JumpingAccTime", jumpingAccelerationTime, TraverseUtils.TraverseAccessType.Property);
    }
}
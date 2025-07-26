using Blasphemous.Framework.Stats.Components;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;

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

        if (!Main.Validate(pcc, x => x != null))
            return;

        platformDropTime = Main.GetValue<float>(pcc, "PlatformDropTime", Main.TraverseAccessType.Property);
        ghostJumpDelay = Main.GetValue<float>(pcc, "GhostJumpDelay", Main.TraverseAccessType.Property);
        ladderJumpTimeThreshold = Main.GetValue<float>(pcc, "m_ladderJumpTimeThreshold", Main.TraverseAccessType.Field);
        climbingSpeed = Main.GetValue<float>(pcc, "ClimbingSpeed", Main.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public override void SetValueTo(PlatformCharacterController pcc)
    {
        base.SetValueTo(pcc);

        if (!Main.Validate(pcc, x => x != null))
            return;

        Main.SetValueIfNotNull(ref pcc, "PlatformDropTime", platformDropTime, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "GhostJumpDelay", ghostJumpDelay, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "m_ladderJumpTimeThreshold", ladderJumpTimeThreshold, Main.TraverseAccessType.Field);
        Main.SetValueIfNotNull(ref pcc, "ClimbingSpeed", climbingSpeed, Main.TraverseAccessType.Property);
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
        if (!Main.Validate(pcc, x => x != null))
            return;

        walkingAcceleration = Main.GetValue<float>(pcc, "WalkingAcc", Main.TraverseAccessType.Property);
        walkingDrag = Main.GetValue<float>(pcc, "WalkingDrag", Main.TraverseAccessType.Property);
        maxWalkingSpeed = Main.GetValue<float>(pcc, "MaxWalkingSpeed", Main.TraverseAccessType.Property);
        airborneAcceleration = Main.GetValue<float>(pcc, "AirborneAcc", Main.TraverseAccessType.Property);
        jumpingSpeed = Main.GetValue<float>(pcc, "JumpingSpeed", Main.TraverseAccessType.Property);
        jumpingAcceleration = Main.GetValue<float>(pcc, "JumpingAcc", Main.TraverseAccessType.Property);
        jumpingAccelerationTime = Main.GetValue<float>(pcc, "JumpingAccTime", Main.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public virtual void SetValueTo(PlatformCharacterController pcc)
    {
        if (!Main.Validate(pcc, x => x != null))
            return;

        Main.SetValueIfNotNull(ref pcc, "WalkingAcc", walkingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "WalkingDrag", walkingDrag, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "AirborneAcc", airborneAcceleration, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "JumpingSpeed", jumpingSpeed, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "JumpingAcc", jumpingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValueIfNotNull(ref pcc, "JumpingAccTime", jumpingAccelerationTime, Main.TraverseAccessType.Property);
    }
}
using Blasphemous.Framework.Stats.Components;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class PlatformCharacterControllerValues_Penitent : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration = Main.DEFAULT_FLOAT;
    public float walkingDrag = Main.DEFAULT_FLOAT;
    public float maxWalkingSpeed = Main.DEFAULT_FLOAT;
    public float airborneAcceleration = Main.DEFAULT_FLOAT;
    public float jumpingSpeed = Main.DEFAULT_FLOAT;
    public float jumpingAcceleration = Main.DEFAULT_FLOAT;
    public float jumpingAccelerationTime = Main.DEFAULT_FLOAT;

    public float platformDropTime = Main.DEFAULT_FLOAT;
    public float ghostJumpDelay = Main.DEFAULT_FLOAT;
    public float ladderJumpTimeThreshold = Main.DEFAULT_FLOAT;
    public float climbingSpeed = Main.DEFAULT_FLOAT;

    /// <inheritdoc/>
    public void GetValueFrom(PlatformCharacterController pcc)
    {
        if (!Main.Validate(pcc, x => x != null))
            return;

        walkingAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "WalkingAcc", Main.TraverseAccessType.Property);
        walkingDrag = Main.GetValue<PlatformCharacterController, float>(pcc, "WalkingDrag", Main.TraverseAccessType.Property);
        maxWalkingSpeed = Main.GetValue<PlatformCharacterController, float>(pcc, "MaxWalkingSpeed", Main.TraverseAccessType.Property);
        airborneAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "AirborneAcc", Main.TraverseAccessType.Property);
        jumpingSpeed = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingSpeed", Main.TraverseAccessType.Property);
        jumpingAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingAcc", Main.TraverseAccessType.Property);
        jumpingAccelerationTime = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingAccTime", Main.TraverseAccessType.Property);

        platformDropTime = Main.GetValue<PlatformCharacterController, float>(pcc, "PlatformDropTime", Main.TraverseAccessType.Property);
        ghostJumpDelay = Main.GetValue<PlatformCharacterController, float>(pcc, "GhostJumpDelay", Main.TraverseAccessType.Property);
        ladderJumpTimeThreshold = Main.GetValue<PlatformCharacterController, float>(pcc, "m_ladderJumpTimeThreshold", Main.TraverseAccessType.Field);
        climbingSpeed = Main.GetValue<PlatformCharacterController, float>(pcc, "ClimbingSpeed", Main.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public void SetValueTo(PlatformCharacterController pcc)
    {
        if (!Main.Validate(pcc, x => x != null))
            return;

        Main.SetValueIfValidated(ref pcc, "WalkingAcc", walkingAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "WalkingDrag", walkingDrag, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "AirborneAcc", airborneAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingSpeed", jumpingSpeed, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAcc", jumpingAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAccTime", jumpingAccelerationTime, Main.IsNotDefault, Main.TraverseAccessType.Property);

        Main.SetValueIfValidated(ref pcc, "PlatformDropTime", platformDropTime, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "GhostJumpDelay", ghostJumpDelay, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "m_ladderJumpTimeThreshold", ladderJumpTimeThreshold, Main.IsNotDefault, Main.TraverseAccessType.Field);
        Main.SetValueIfValidated(ref pcc, "ClimbingSpeed", climbingSpeed, Main.IsNotDefault, Main.TraverseAccessType.Property);
    }
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Enemy"/>
/// </summary>
public class PlatformCharacterControllerValues_Enemy : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration = Main.DEFAULT_FLOAT;
    public float walkingDrag = Main.DEFAULT_FLOAT;
    public float maxWalkingSpeed = Main.DEFAULT_FLOAT;
    public float airborneAcceleration = Main.DEFAULT_FLOAT;
    public float jumpingSpeed = Main.DEFAULT_FLOAT;
    public float jumpingAcceleration = Main.DEFAULT_FLOAT;
    public float jumpingAccelerationTime = Main.DEFAULT_FLOAT;

    /// <inheritdoc/>
    public void GetValueFrom(PlatformCharacterController pcc)
    {
        if (!Main.Validate(pcc, x => x != null))
            return;

        walkingAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "WalkingAcc", Main.TraverseAccessType.Property);
        walkingDrag = Main.GetValue<PlatformCharacterController, float>(pcc, "WalkingDrag", Main.TraverseAccessType.Property);
        maxWalkingSpeed = Main.GetValue<PlatformCharacterController, float>(pcc, "MaxWalkingSpeed", Main.TraverseAccessType.Property);
        airborneAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "AirborneAcc", Main.TraverseAccessType.Property);
        jumpingSpeed = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingSpeed", Main.TraverseAccessType.Property);
        jumpingAcceleration = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingAcc", Main.TraverseAccessType.Property);
        jumpingAccelerationTime = Main.GetValue<PlatformCharacterController, float>(pcc, "JumpingAccTime", Main.TraverseAccessType.Property);
    }

    /// <inheritdoc/>
    public void SetValueTo(PlatformCharacterController pcc)
    {
        if (!Main.Validate(pcc, x => x != null))
            return;

        Main.SetValueIfValidated(ref pcc, "WalkingAcc", walkingAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "WalkingDrag", walkingDrag, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "AirborneAcc", airborneAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingSpeed", jumpingSpeed, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAcc", jumpingAcceleration, Main.IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAccTime", jumpingAccelerationTime, Main.IsNotDefault, Main.TraverseAccessType.Property);
    }
}
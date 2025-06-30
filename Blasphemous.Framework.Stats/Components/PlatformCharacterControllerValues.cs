using Blasphemous.Framework.Stats.Components;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;
using UnityEngine;

namespace Blasphemous.Framework.Stats.Extensions;

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class PlatformCharacterControllerValues_Penitent : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration = DEFAULT_FLOAT;
    public float walkingDrag = DEFAULT_FLOAT;
    public float maxWalkingSpeed = DEFAULT_FLOAT;
    public float airborneAcceleration = DEFAULT_FLOAT;
    public float jumpingSpeed = DEFAULT_FLOAT;
    public float jumpingAcceleration = DEFAULT_FLOAT;
    public float jumpingAccelerationTime = DEFAULT_FLOAT;

    public float platformDropTime = DEFAULT_FLOAT;
    public float ghostJumpDelay = DEFAULT_FLOAT;
    public float ladderJumpTimeThreshold = DEFAULT_FLOAT;
    public float climbingSpeed = DEFAULT_FLOAT;

    internal const float DEFAULT_FLOAT = -114514f;
    internal const int DEFAULT_INT = -114514;

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

        Main.SetValueIfValidated(ref pcc, "WalkingAcc", walkingAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "WalkingDrag", walkingDrag, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "AirborneAcc", airborneAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingSpeed", jumpingSpeed, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAcc", jumpingAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAccTime", jumpingAccelerationTime, IsNotDefault, Main.TraverseAccessType.Property);

        Main.SetValueIfValidated(ref pcc, "PlatformDropTime", platformDropTime, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "GhostJumpDelay", ghostJumpDelay, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "m_ladderJumpTimeThreshold", ladderJumpTimeThreshold, IsNotDefault, Main.TraverseAccessType.Field);
        Main.SetValueIfValidated(ref pcc, "ClimbingSpeed", climbingSpeed, IsNotDefault, Main.TraverseAccessType.Property);
    }

    private bool IsNotDefault<T>(T obj)
    {
        switch (obj)
        {
            case float value:
                return !Mathf.Approximately((float)value, DEFAULT_FLOAT);
            case int value:
                return !Mathf.Approximately((int)value, DEFAULT_INT);
            default:
                return false;
        }
    }
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Enemy"/>
/// </summary>
public class PlatformCharacterControllerValues_Enemy : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration = DEFAULT_FLOAT;
    public float walkingDrag = DEFAULT_FLOAT;
    public float maxWalkingSpeed = DEFAULT_FLOAT;
    public float airborneAcceleration = DEFAULT_FLOAT;
    public float jumpingSpeed = DEFAULT_FLOAT;
    public float jumpingAcceleration = DEFAULT_FLOAT;
    public float jumpingAccelerationTime = DEFAULT_FLOAT;

    internal const float DEFAULT_FLOAT = -114514f;
    internal const int DEFAULT_INT = -114514;

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

        Main.SetValueIfValidated(ref pcc, "WalkingAcc", walkingAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "WalkingDrag", walkingDrag, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "AirborneAcc", airborneAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingSpeed", jumpingSpeed, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAcc", jumpingAcceleration, IsNotDefault, Main.TraverseAccessType.Property);
        Main.SetValueIfValidated(ref pcc, "JumpingAccTime", jumpingAccelerationTime, IsNotDefault, Main.TraverseAccessType.Property);
    }

    private bool IsNotDefault<T>(T obj)
    {
        switch (obj)
        {
            case float value:
                return !Mathf.Approximately((float)value, DEFAULT_FLOAT);
            case int value:
                return !Mathf.Approximately((int)value, DEFAULT_INT);
            default:
                return false;
        }
    }
}
using Blasphemous.Framework.Stats.Components;
using Blasphemous.ModdingAPI;
using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;

namespace Blasphemous.Framework.Stats.Extensions;

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class PlatformCharacterControllerValues_Penitent : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration;
    public float walkingDrag;
    public float maxWalkingSpeed;
    public float airborneAcceleration;
    public float jumpingSpeed;
    public float jumpingAcceleration;
    public float jumpingAccelerationTime;

    public float platformDropTime;
    public float ghostJumpDelay;
    public float ladderJumpTimeThreshold;
    public float climbingSpeed;

    /// <inheritdoc/>
    public void GetValueFrom(PlatformCharacterController pcc)
    {
        if (pcc == null)
        {
            ModLog.Warn($"PlatformCharacterController {pcc} is null!");
            return;
        }

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
        if (pcc == null)
        {
            ModLog.Warn($"PlatformCharacterController {pcc} is null!");
            return;
        }

        Main.SetValue(ref pcc, "WalkingAcc", walkingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "WalkingDrag", walkingDrag, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "AirborneAcc", airborneAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingSpeed", jumpingSpeed, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingAcc", jumpingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingAccTime", jumpingAccelerationTime, Main.TraverseAccessType.Property);

        Main.SetValue(ref pcc, "PlatformDropTime", platformDropTime, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "GhostJumpDelay", ghostJumpDelay, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "m_ladderJumpTimeThreshold", ladderJumpTimeThreshold, Main.TraverseAccessType.Field);
        Main.SetValue(ref pcc, "ClimbingSpeed", climbingSpeed, Main.TraverseAccessType.Property);
    }
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Enemy"/>
/// </summary>
public class PlatformCharacterControllerValues_Enemy : IAccessible<PlatformCharacterController>
{
    public float walkingAcceleration;
    public float walkingDrag;
    public float maxWalkingSpeed;
    public float airborneAcceleration;
    public float jumpingSpeed;
    public float jumpingAcceleration;
    public float jumpingAccelerationTime;

    /// <inheritdoc/>
    public void GetValueFrom(PlatformCharacterController pcc)
    {
        Main.Validate(pcc, x => x != null);

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
        Main.Validate(pcc, x => x != null);

        Main.SetValue(ref pcc, "WalkingAcc", walkingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "WalkingDrag", walkingDrag, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "MaxWalkingSpeed", maxWalkingSpeed, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "AirborneAcc", airborneAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingSpeed", jumpingSpeed, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingAcc", jumpingAcceleration, Main.TraverseAccessType.Property);
        Main.SetValue(ref pcc, "JumpingAccTime", jumpingAccelerationTime, Main.TraverseAccessType.Property);
    }
}
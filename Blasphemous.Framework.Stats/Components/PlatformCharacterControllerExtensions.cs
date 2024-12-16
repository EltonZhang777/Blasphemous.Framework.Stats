using CreativeSpore.SmartColliders;
using Gameplay.GameControllers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Useful extensions for inspecting <see cref="PlatformCharacterController"/>
/// </summary>
public static class PlatformCharacterControllerExtensions
{
    /// <summary>
    /// Write the useful values of the given attribute to an <see cref="PlatformCharacterControllerValues_Enemy"/> instance
    /// </summary>
    public static PlatformCharacterControllerValues_Enemy ToPlatformCharacterControllerValues_Enemy(this PlatformCharacterController pcc)
    {
        return new PlatformCharacterControllerValues_Enemy()
        {
            walkingAcceleration = pcc.WalkingAcc,
            walkingDrag = pcc.WalkingDrag,
            maxWalkingSpeed = pcc.MaxWalkingSpeed,
            airborneAcceleration = pcc.AirborneAcc,
            jumpingSpeed = pcc.JumpingSpeed,
            jumpingAcceleration = pcc.JumpingAcc,
            jumpingAccelerationTime = pcc.JumpingAccTime,
        };
    }

    /// <summary>
    /// Write the useful values of the given attribute to an <see cref="PlatformCharacterControllerValues_Penitent"/> instance
    /// </summary>
    public static PlatformCharacterControllerValues_Penitent ToPlatformCharacterControllerValues_Penitent(this PlatformCharacterController pcc)
    {
        return new PlatformCharacterControllerValues_Penitent()
        {
            walkingAcceleration = pcc.WalkingAcc,
            walkingDrag = pcc.WalkingDrag,
            maxWalkingSpeed = pcc.MaxWalkingSpeed,
            airborneAcceleration = pcc.AirborneAcc,
            jumpingSpeed = pcc.JumpingSpeed,
            jumpingAcceleration = pcc.JumpingAcc,
            jumpingAccelerationTime = pcc.JumpingAccTime,

            platformDropTime = pcc.PlatformDropTime,
            ghostJumpDelay = pcc.GhostJumpDelay,
            ladderJumpTimeThreshold = pcc.LadderJumpTimeThreshold,
            climbingSpeed = pcc.ClimbingSpeed,
        };
    }
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Gameplay.GameControllers.Penitent.Penitent"/>
/// </summary>
public class PlatformCharacterControllerValues_Penitent
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
}

/// <summary>
/// Documenting useful values of a <see cref="PlatformCharacterController"/> for <see cref="Enemy"/>
/// </summary>
public class PlatformCharacterControllerValues_Enemy
{
    public float walkingAcceleration;
    public float walkingDrag;
    public float maxWalkingSpeed;
    public float airborneAcceleration;
    public float jumpingSpeed;
    public float jumpingAcceleration;
    public float jumpingAccelerationTime;
}
using BepInEx;
using Blasphemous.ModdingAPI;
using Framework.FrameworkCore;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Blasphemous.Framework.Stats;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.4.1")]
[BepInDependency("Blasphemous.Framework.UI", "0.1.2")]

internal class Main : BaseUnityPlugin
{
    public enum TraverseAccessType
    {
        Field,
        Property
    }

    public static StatsFramework StatsFramework { get; private set; }

    private void Start()
    {
        StatsFramework = new StatsFramework();
    }

    /// <summary>
    /// Get value from a field/property of a traverse instance, regardless of accessibility levels
    /// </summary>
    public static TValue GetValue<TValue>(Traverse traverse, string variableName, TraverseAccessType accessType)
    {
        if (traverse == null)
        {
            ModLog.Error($"Failed to get null value from null traverse instance! Returning default");
            return default(TValue);
        }
        variableName = variableName.Trim();
        return accessType switch
        {
            TraverseAccessType.Field => traverse.Field(variableName).GetValue<TValue>(),
            TraverseAccessType.Property => traverse.Property(variableName).GetValue<TValue>(),
            _ => default(TValue)
        };
    }

    /// <summary>
    /// Traverse and get value of a variable, regardless of accessibility levels
    /// </summary>
    public static TValue GetValue<TValue>(object obj, string variableName, TraverseAccessType accessType)
    {
        Traverse traverse = Traverse.Create(obj);
        if (traverse == null)
        {
            ModLog.Error($"Failed to get null value from object of type `{obj.GetType()}`! Returning default");
            return default(TValue);
        }
        return GetValue<TValue>(traverse, variableName, accessType);
    }

    /// <summary>
    /// Set value to a field/property of a traverse instance, regardless of accessibility levels
    /// </summary>
    public static void SetValue<TValue>(ref Traverse traverse, string variableName, TValue value, TraverseAccessType accessType)
    {
        variableName = variableName.Trim();
        switch (accessType)
        {
            case TraverseAccessType.Field:
                traverse.Field(variableName).SetValue(value);
                break;
            case TraverseAccessType.Property:
                traverse.Property(variableName).SetValue(value);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// Traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValue<TTarget, TValue>(ref TTarget obj, string variableName, TValue value, TraverseAccessType accessType)
    {
        Traverse traverse = Traverse.Create(obj);
        SetValue<TValue>(ref traverse, variableName, value, accessType);
    }

    /// <summary>
    /// Validate if the given value satisfies the given restriction, 
    /// then traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfValidated<TTarget, TValue>(ref TTarget obj, string variableName, TValue value, Func<TValue, bool> validate, TraverseAccessType accessType)
    {
        if (!validate(value))
        {
            return;
        }
        SetValue(ref obj, variableName, value, accessType);
    }

    /// <summary>
    /// Validate if the given value satisfies the given restriction, 
    /// then set value of a traverse instance, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfValidated<TValue>(ref Traverse traverse, string variableName, TValue value, Func<TValue, bool> validate, TraverseAccessType accessType)
    {
        if (!validate(value))
        {
            return;
        }
        SetValue<TValue>(ref traverse, variableName, value, accessType);
    }

    /// <summary>
    /// Validate if the given value satisfies all given restrictions, 
    /// then traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfValidated<TTarget, TValue>(ref TTarget obj, string variableName, TValue value, List<Func<TValue, bool>> validates, TraverseAccessType accessType)
    {
        foreach (Func<TValue, bool> validate in validates)
        {
            if (!validate(value))
            {
                return;
            }
        }
        SetValue(ref obj, variableName, value, accessType);
    }

    /// <summary>
    /// Validate if the given nullable struct value is not null, 
    /// then set value of the traverse instance, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfNotNull<TValue>(ref Traverse traverse, string variableName, TValue? value, TraverseAccessType accessType)
        where TValue : struct
    {
        if (!value.HasValue)
        {
            return;
        }
        SetValue<TValue>(ref traverse, variableName, value.Value, accessType);
    }

    /// <summary>
    /// Validate if the given nullable struct value is not null, 
    /// then traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfNotNull<TTarget, TValue>(ref TTarget obj, string variableName, TValue? value, TraverseAccessType accessType)
        where TValue : struct
    {
        if (!value.HasValue)
        {
            return;
        }
        SetValue(ref obj, variableName, value.Value, accessType);
    }

    /// <summary>
    /// Validate if the given nullable class value is not null, 
    /// then set value of the traverse instance, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfNotNull<TValue>(ref Traverse traverse, string variableName, TValue value, TraverseAccessType accessType)
        where TValue : class
    {
        if (value == null)
        {
            return;
        }
        SetValue<TValue>(ref traverse, variableName, value, accessType);
    }

    /// <summary>
    /// Validate if the given nullable class value is not null, 
    /// then traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValueIfNotNull<TTarget, TValue>(ref TTarget obj, string variableName, TValue value, TraverseAccessType accessType)
        where TValue : class
    {
        if (value == null)
        {
            return;
        }
        SetValue(ref obj, variableName, value, accessType);
    }

    /// <summary>
    /// Send an error log (or throw an error) if the given object does not satisfy the given restrictions
    /// </summary>
    public static bool Validate<T>(T obj, Func<T, bool> validate, bool throwError = false)
    {
        if (!validate(obj))
        {
            string errorMessage = $"`{obj}` of type `{typeof(T)}` isn't a valid argument";
            ArgumentException exception = new ArgumentException(errorMessage);
            ModLog.Error(errorMessage);
            if (throwError)
                throw exception;
        }
        return validate(obj);
    }

    internal static void EnsureDirectoryExists(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// Return a normalized directional <see cref="Vector2"/> based on the <see cref="EntityOrientation"/> given.
    /// </summary>
    /// <param name="entityOrientation">The given orientation</param>
    /// <returns><see cref="Vector2.left"/> if <see cref="EntityOrientation.Left"/>, <see cref="Vector2.right"/> if <see cref="EntityOrientation.Right"/></returns>
    internal static Vector2 EntityOrientationToDirectionalVector(EntityOrientation entityOrientation)
    {
        return entityOrientation switch
        {
            EntityOrientation.Right => Vector2.right,
            EntityOrientation.Left => Vector2.left
        };
    }
}

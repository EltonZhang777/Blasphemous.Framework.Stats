using BepInEx;
using Blasphemous.ModdingAPI;
using HarmonyLib;
using System;
using System.Collections.Generic;

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
    /// Traverse and get value of a variable, regardless of accessibility levels
    /// </summary>
    public static TValue GetValue<TTarget, TValue>(TTarget obj, string variableName, TraverseAccessType accessType)
    {
        Traverse traverse = Traverse.Create(obj);
        return accessType switch
        {
            TraverseAccessType.Field => traverse.Field(variableName).GetValue<TValue>(),
            TraverseAccessType.Property => traverse.Property(variableName).GetValue<TValue>(),
            _ => default(TValue)
        };
    }

    /// <summary>
    /// Traverse and set value of a variable, regardless of accessibility levels
    /// </summary>
    public static void SetValue<TTarget, TValue>(ref TTarget obj, string variableName, TValue value, TraverseAccessType accessType)
    {
        Traverse traverse = Traverse.Create(obj);
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
            ModLog.Error(errorMessage);
            if (throwError)
                throw new ArgumentException(errorMessage);
        }
        return validate(obj);
    }
}

using BepInEx;
using Blasphemous.ModdingAPI;
using HarmonyLib;
using System;

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
    /// Send an error log (or throw an error) if the given object does not satisfy the given restrictions
    /// </summary>
    public static T Validate<T>(T obj, Func<T, bool> validate, bool throwError = false)
    {
        if (!validate(obj))
        {
            string errorMessage = $"`{obj}` of type `{typeof(T)}` isn't a valid argument";
            ModLog.Error(errorMessage);
            if (throwError)
                throw new ArgumentException(errorMessage);
        }
        return obj;
    }
}

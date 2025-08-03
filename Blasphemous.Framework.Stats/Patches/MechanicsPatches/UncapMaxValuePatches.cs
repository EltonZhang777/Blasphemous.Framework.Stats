using Framework.FrameworkCore.Attributes.Logic;
using HarmonyLib;

namespace Blasphemous.Framework.Stats.Patches.MechanicsPatches;

/// <summary>
/// Uncap the max value of <see cref="VariableAttribute"/>
/// </summary>
[HarmonyPatch(typeof(VariableAttribute), "MaxValue", MethodType.Getter)]
class VariableAttribute_MaxValue_UncapMaxValue_Patch
{
    public static bool Prefix(
        ref float __result)
    {
        __result = float.MaxValue;
        return false;
    }
}
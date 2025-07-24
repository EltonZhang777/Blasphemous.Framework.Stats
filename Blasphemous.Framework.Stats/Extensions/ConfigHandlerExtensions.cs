using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Config;
using Blasphemous.ModdingAPI.Files;
using Newtonsoft.Json;
using System.IO;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class ConfigHandlerExtensions
{
    private static BlasMod GetMod(this ConfigHandler configHandler) => Main.GetValue<ConfigHandler, BlasMod>(configHandler, "_mod", Main.TraverseAccessType.Field);

    /// <summary>
    /// Load config while specifying <see cref="JsonSerializerSettings"/>
    /// </summary>
    internal static T Load<T>(this ConfigHandler configHandler, JsonSerializerSettings settings = null) where T : new()
    {
        FileHandler fileHandler = configHandler.GetMod().FileHandler;
        string text = fileHandler.ReadFileContents(fileHandler.GetConfigPath(), out string output) ? output : string.Empty;
        if (text == string.Empty)
        {
            T val = new();
            configHandler.Save(val);
            return val;
        }

        return JsonConvert.DeserializeObject<T>(text, settings);
    }

    /// <summary>
    /// Save config while specifying <see cref="JsonSerializerSettings"/>
    /// </summary>
    internal static void Save<T>(this ConfigHandler configHandler, T config, Formatting formatting = Formatting.Indented, JsonSerializerSettings settings = null)
    {
        FileHandler fileHandler = configHandler.GetMod().FileHandler;
        Main.EnsureDirectoryExists(fileHandler.GetConfigPath());

        string outputContents = JsonConvert.SerializeObject(config, formatting, settings);
        File.WriteAllText(fileHandler.GetConfigPath(), outputContents);
    }
}

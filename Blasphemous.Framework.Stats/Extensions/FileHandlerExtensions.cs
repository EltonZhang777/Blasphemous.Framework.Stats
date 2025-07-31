using Blasphemous.ModdingAPI.Files;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class FileHandlerExtensions
{
    internal static string GetDataPath(this FileHandler fileHandler)
    {
        return Main.GetValue<string>(fileHandler, "dataPath", Main.TraverseAccessType.Field);
    }

    internal static string GetConfigPath(this FileHandler fileHandler)
    {
        return Main.GetValue<string>(fileHandler, "configPath", Main.TraverseAccessType.Field);
    }

    internal static string[] GetAllDataFileNames(this FileHandler fileHandler)
    {
        return Directory.GetFiles(fileHandler.GetDataPath()).Select(x => Path.GetFileName(x)).ToArray();
    }

    internal static T LoadDataAsJson<T>(this FileHandler fileHandler, string fileName, JsonSerializerSettings settings = null)
    {
        if (!INTERNAL_CALL_LoadDataAsJson(fileName, out T result))
        {
            throw new ArgumentException($"Failed to load {fileName} to JSON of type {typeof(T)}!");
        }
        return result;

        bool INTERNAL_CALL_LoadDataAsJson<T1>(string fileName, out T1 output)
        {
            if (fileHandler.ReadFileContents(Path.Combine(fileHandler.GetDataPath(), fileName), out var output2))
            {
                output = JsonConvert.DeserializeObject<T1>(output2, settings);
                return true;
            }

            output = default(T1);
            return false;
        }
    }

    internal static bool LoadContentAsJson<T>(this FileHandler fileHandler, string fileName, out T output)
    {
        if (ReadFileContents(fileHandler, Path.Combine(fileHandler.ContentFolder, fileName), out var output2))
        {
            output = JsonConvert.DeserializeObject<T>(output2);
            return true;
        }

        output = default(T);
        return false;
    }

    internal static void WriteJsonToContent(this FileHandler fileHandler, string fileName, object obj, JsonSerializerSettings settings = null, Formatting formatting = Formatting.Indented)
    {
        if (settings != null)
        {
            File.WriteAllText(
                Path.Combine(fileHandler.ContentFolder, fileName),
                JsonConvert.SerializeObject(obj, formatting, settings));
        }
        else
        {
            File.WriteAllText(
                Path.Combine(fileHandler.ContentFolder, fileName),
                JsonConvert.SerializeObject(obj, formatting));
        }
    }

    internal static bool ReadFileContents(this FileHandler fileHandler, string path, out string output)
    {
        if (File.Exists(path))
        {
            output = File.ReadAllText(path);
            return true;
        }

        output = null;
        return false;
    }
}

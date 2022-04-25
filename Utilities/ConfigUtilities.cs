using Aerolt.Options;
using Newtonsoft.Json;
using RoR2;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace Aerolt.Utilities
{
    public class ConfigUtilities
    {
        public static string SelectedConfig = "default";
        private static string ConfigPath = Application.dataPath + "/AeroltConfigs/";
        private static string GetConfigPath(string name = "default") { return ConfigPath + name + ".config"; }
        public static void CreateEnvironment()
        {
            if (!Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }
            if (!File.Exists(GetConfigPath()))
                SaveConfig();
            else
                LoadConfig();
        }
        public static void SaveConfig(string name = "default", bool setconfig = false)
        {
            string path = GetConfigPath(name);
            string json = JsonConvert.SerializeObject(G.Settings, Formatting.Indented);
            File.WriteAllText(path, json);
            if (setconfig)
                SelectedConfig = name;
            if (G._CharacterCollected)
                Chat.AddMessage($"Saved Config {name}");
        }
        public static void LoadConfig(string name = "default")
        {
            string json = File.ReadAllText(GetConfigPath(name));
            Config s = JsonConvert.DeserializeObject<Config>(json);
            G.Settings = s;
            SelectedConfig = name;
            if (G._CharacterCollected)
                Chat.AddMessage($"Loaded Config {name}");
            Colors.AddColors();
        }
        public static List<string> GetConfigs(bool Extensions = false)
        {
            List<string> files = new List<string>();
            DirectoryInfo d = new DirectoryInfo(ConfigPath);
            FileInfo[] Files = d.GetFiles("*.config");
            foreach (FileInfo file in Files)
            {
                if (Extensions)
                    files.Add(file.Name.Substring(0, file.Name.Length));
                else
                    files.Add(file.Name.Substring(0, file.Name.Length - 7));
            }
            return files;
        }
    }
}

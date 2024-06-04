using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StarterKit.Database
{
    internal struct RecordKit
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public RecordKit(string name, int amount) { Name = name; Amount = amount; }
    }
    internal class DB
    {
        private static readonly string FileDirectory = Path.Combine("BepInEx", "config", MyPluginInfo.PLUGIN_NAME);
        private static readonly string FileStartKits = "StartKits.json";
        private static readonly string FileUsedKits = "UsedKits.json";
        private static readonly string PathStarterKits = Path.Combine(FileDirectory, FileStartKits);
        private static readonly string PathUsedKits = Path.Combine(FileDirectory, FileUsedKits);

        public static ConcurrentDictionary<string, List<RecordKit>> StartKits = new ConcurrentDictionary<string, List<RecordKit>>();
        public static ConcurrentDictionary<ulong, bool> UsedKits = new ConcurrentDictionary<ulong, bool>();
        public static bool EnabledKitCommand = true;
        public static string MessageAlreadyUsedKit = "";
        public static string MessageOnGivenKit = "";
        internal static void SaveData()
        {
            File.WriteAllText(PathStarterKits, JsonSerializer.Serialize(DB.StartKits, new JsonSerializerOptions() { WriteIndented = true }));
            File.WriteAllText(PathUsedKits, JsonSerializer.Serialize(DB.UsedKits, new JsonSerializerOptions() { WriteIndented = true }));
            Plugin.Logger.LogWarning("StartKit, UsedKits DB Saved.");
        }
        internal static void LoadData()
        {
            if (!Directory.Exists(FileDirectory)) Directory.CreateDirectory(FileDirectory);
            LoadStarterKit();
            LoadUsedKits();
            SaveData();
        }
        internal static void LoadUsedKits()
        {
            if (!File.Exists(PathUsedKits))
            {
                DB.UsedKits.Clear();
                Plugin.Logger.LogWarning("UsedKits DB Created.");
            }
            else
            {
                string json = File.ReadAllText(PathUsedKits);
                DB.UsedKits = JsonSerializer.Deserialize<ConcurrentDictionary<ulong, bool>>(json);
                Plugin.Logger.LogWarning("UsedKits DB Populated");
            }
        }
        internal static void LoadStarterKit()
        {
            if (!File.Exists(PathStarterKits))
            {
                DB.StartKits.Clear();
                DB.StartKits.TryAdd("startkit", new List<RecordKit>() { (new RecordKit("Item_Boots_T09_Dracula_Brute", 1)), (new RecordKit("Item_Chest_T09_Dracula_Brute", 1)), (new RecordKit("Item_Gloves_T09_Dracula_Brute", 1)), (new RecordKit("Item_Legs_T09_Dracula_Brute", 1)) });
                Plugin.Logger.LogWarning("StartKit DB Created.");
            }
            else
            {
                string json = File.ReadAllText(PathStarterKits);
                DB.StartKits = JsonSerializer.Deserialize<ConcurrentDictionary<string, List<RecordKit>>>(json);
                Plugin.Logger.LogWarning("StartKit DB Populated");
            }
        }
    }

}

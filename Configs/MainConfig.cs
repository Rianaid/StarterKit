using BepInEx.Configuration;
using StarterKit.Database;
using System.IO;

namespace StarterKit.Configs
{
    internal class MainConfig
    {
        private static readonly string FileDirectory = Path.Combine("BepInEx", "config");
        private static readonly string FileName = "StarterKit.cfg";
        private static readonly string fullPath = Path.Combine(FileDirectory, FileName);
        private static readonly ConfigFile Conf = new ConfigFile(fullPath, true);

        public static ConfigEntry<string> MessageOnGivenKit;
        public static ConfigEntry<string> MessageAlreadyUsedKit;
        public static ConfigEntry<bool> EnabledKitCommand;


        public static void ConfigInit()
        {
            //--------ShardAmount----------
            EnabledKitCommand = Conf.Bind("StarterKit", "EnableKitCommand", true, "Enable kit command..");
            MessageOnGivenKit = Conf.Bind("StarterKit", "MessageOnGivenKit", "Enjoy your <color=#ffffffff>free gear pack</color> and let the battle begin!", "Message when kit given player.");
            MessageAlreadyUsedKit = Conf.Bind("StarterKit", "MessageAlreadyUsedKit", "You have already used the starter kit.", "Message when the player has already used a kit and will try to use it again.");

            ConfigBind();
        }
        public static void ConfigBind()
        {
            DB.EnabledKitCommand = EnabledKitCommand.Value;
            DB.MessageAlreadyUsedKit = MessageAlreadyUsedKit.Value;
            DB.MessageOnGivenKit = MessageOnGivenKit.Value;
        }
    }
}

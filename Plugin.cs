﻿using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using StarterKit.Configs;
using StarterKit.Database;
using VampireCommandFramework;

namespace StarterKit
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("gg.deca.VampireCommandFramework")]
    public class Plugin : BasePlugin
    {
        Harmony _harmony;
        public static ManualLogSource Logger;
        internal static Plugin Instance;
        public override void Load()
        {
            Instance = this;
            Logger = Log;
            // Plugin startup logic
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

            // Harmony patching
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
            MainConfig.ConfigInit();
            DB.LoadData();
            // Register all commands in the assembly with VCF
            CommandRegistry.RegisterAll();
        }

        public override bool Unload()
        {
            CommandRegistry.UnregisterAssembly();
            _harmony?.UnpatchSelf();
            return true;
        }

        // // Uncomment for example commmand or delete

        // /// <summary> 
        // /// Example VCF command that demonstrated default values and primitive types
        // /// Visit https://github.com/decaprime/VampireCommandFramework for more info 
        // /// </summary>
        // /// <remarks>
        // /// How you could call this command from chat:
        // ///
        // /// .startingkit-example "some quoted string" 1 1.5
        // /// .startingkit-example boop 21232
        // /// .startingkit-example boop-boop
        // ///</remarks>
        // [Command("startingkit-example", description: "Example command from startingkit", adminOnly: true)]
        // public void ExampleCommand(ICommandContext ctx, string someString, int num = 5, float num2 = 1.5f)
        // { 
        //     ctx.Reply($"You passed in {someString} and {num} and {num2}");
        // }
    }
}

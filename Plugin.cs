using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using NSilkAPI.API;
using SilkAPI;
using System;

namespace SilkAPIPlugin;

//  [BepInPlugin("org.silkapi.core", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInPlugin("org.silkapi.core", MyPluginInfo.PLUGIN_NAME, "0.0.1")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    internal static SilkUIManager ui_manager;
        
    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin org.silkapi.core is loaded!");

        var Harmony = new Harmony("org.silkapi.core");

        Harmony.PatchAll();

        SilkApi.instance.Registries.UI.Menu.RegisterMenuItem("Mods");
        SilkApi.instance.Registries.UI.VersionShower.RegisterVersionString($"SilkAPI {MyPluginInfo.PLUGIN_VERSION}");
    }
}
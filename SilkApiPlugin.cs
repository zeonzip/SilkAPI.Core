using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SilkAPI.API;
using SilkAPI;
using SilkAPI.API.PluginLoading;

namespace SilkAPIPlugin;

[BepInAutoPlugin("org.silkapi.core", MyPluginInfo.PLUGIN_NAME, "0.0.1")]
public partial class SilkApiPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    internal static SilkUIManager ui_manager;
    public static SilkApiPlugin Instance;
    public static SilkPluginManager PluginManager;
    
    public Harmony Harmony = new(Id);
    
    private void Awake()
    {
        Instance = this;
        Logger = base.Logger;
        PluginManager = new();
        Logger.LogInfo($"Plugin org.silkapi.core is loaded!");
        Harmony.PatchAll();

        SilkApi.instance.Registries.UI.Menu.RegisterMenuItem("Mods");
        SilkApi.instance.Registries.UI.VersionShower.RegisterVersionString($"SilkAPI {MyPluginInfo.PLUGIN_VERSION}");
    }

    private void Start()
    {
        PluginManager.Start();
    }
}
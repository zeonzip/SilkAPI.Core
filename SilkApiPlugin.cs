using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SilkAPI.API;
using SilkAPI;
using SilkAPI.PluginLoading;
using SilkAPI.UI;

namespace SilkAPIPlugin;

[BepInAutoPlugin("org.silkapi.core", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public partial class SilkApiPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    internal static SilkUIManager UIManager;
    public static SilkApiPlugin Instance;
    public static SilkPluginManager PluginManager;
    
    public Harmony Harmony = new(Id);
    
    private void Awake()
    {
        Instance = this;
        Logger = base.Logger;
        PluginManager = new();
        Logger.LogInfo($"{SilkApiPlugin.Name} v{SilkApiPlugin.Version} loaded!");
        Harmony.PatchAll();

        SilkApi.instance.Registries.UI.Menu.RegisterMenuItem(new SilkButton("Mods", evnt =>
        {
            // placeholder
            Logger.LogInfo($"Mods button interacted with!");
        }));
        SilkApi.instance.Registries.UI.VersionShower.RegisterVersionString($"SilkAPI {MyPluginInfo.PLUGIN_VERSION}");
    }

    private void Start()
    {
        PluginManager.Start();
    }
}
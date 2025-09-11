using BepInEx;
using BepInEx.Configuration;

namespace SilkAPI.API.PluginLoading;

public class SilkPluginInfo : PluginInfo
{
    internal SilkPluginInfo(ISilkPlugin silkPlugin, PluginInfo info)
    {
        SilkPlugin = silkPlugin;
        PluginInfo = info;
        PluginConfig = silkPlugin.GetConfigFile();
        PluginId = info.Metadata.GUID;
    }
    public ISilkPlugin SilkPlugin { get; }
    public PluginInfo PluginInfo { get; }
    public ConfigFile PluginConfig { get; }
    public string PluginId { get; }
}

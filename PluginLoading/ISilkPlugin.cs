using BepInEx.Configuration;

namespace SilkAPI.PluginLoading;

public interface ISilkPlugin
{
    string OptionsTitleText { get; }
    public ConfigFile GetConfigFile();
}
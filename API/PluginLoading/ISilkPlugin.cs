using BepInEx.Configuration;

namespace SilkAPI.API.PluginLoading;

public interface ISilkPlugin
{
    string OptionsTitleText { get; }
    public ConfigFile GetConfigFile();
}
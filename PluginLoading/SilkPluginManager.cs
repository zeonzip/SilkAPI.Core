using System.Collections.Generic;
using System.Reflection;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using SilkAPIPlugin;

namespace SilkAPI.PluginLoading;

public sealed class SilkPluginManager
{
    internal static SilkPluginManager Instance { get; private set; } = new();
    internal List<SilkPluginInfo> RegisteredPlugins { get; private set; } = new();

    public SilkPluginInfo[] GetRegisteredPlugins()
    {
        return RegisteredPlugins.ToArray();
    }

    private readonly ManualLogSource _log;
    internal SilkPluginManager()
    {
        Instance = this;
        _log = SilkApiPlugin.Logger;
    }

    public void Start()
    {
        
        foreach (var pluginInfo in Chainloader.PluginInfos)
        {
            // Register in mod list

            if (pluginInfo.Key == SilkApiPlugin.Id) // This check is because SilkApi will also be in the list of registered Plugins, and we want to skip it
            {
                continue;
            }
      
            if (pluginInfo.Value.Instance is not ISilkPlugin plugin) 
            { 
                continue;
            }

            var info = new SilkPluginInfo(plugin, pluginInfo.Value);
            var assembly = plugin.GetType().Assembly;

            info.PluginConfig.SettingChanged += (sender, args) => { };

            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<SilkIgnoreAttribute>() != null)
                {
                    continue;
                }
                
                // Other option stuff here
            }
            
            var metadata = pluginInfo.Value.Metadata;
            RegisteredPlugins.Add(info);
            _log.LogInfo($"Loaded plugin {metadata.Name} v{metadata.Version}");
        }

        if (RegisteredPlugins.Count == 0)
        {
            _log.LogWarning("No plugins were loaded");
        }

    }
}


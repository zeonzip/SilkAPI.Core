using System;
using System.Reflection;

namespace SilkAPI.PluginLoading.Events;

public class PluginLoadedEventArgs : EventArgs
{
    public PluginLoadedEventArgs(Assembly pluginAssembly, SilkPluginInfo pluginInfo)
    {
        Assembly = pluginAssembly;
        SilkPluginInfo = pluginInfo;
    }

    public SilkPluginInfo SilkPluginInfo { get; }
    public Assembly Assembly { get; }
}
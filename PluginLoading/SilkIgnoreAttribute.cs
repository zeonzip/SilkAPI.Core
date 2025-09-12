using System;

namespace SilkAPI.PluginLoading;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false)]
public class SilkIgnoreAttribute : Attribute;
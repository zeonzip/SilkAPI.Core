using System;

namespace SilkAPI.API.PluginLoading;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false)]
public class SilkIgnoreAttribute : Attribute;
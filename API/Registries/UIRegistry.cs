using System.Collections.Generic;
using HarmonyLib;
using SilkAPI.UI;
using UnityEngine.UI;

namespace SilkAPI.API.Registries
{
    
    public class UIRegistry
    {
        public MainMenuRegistry Menu = new MainMenuRegistry();
        public VersionShowerRegistry VersionShower = new VersionShowerRegistry();
    }

    public class MainMenuRegistry
    {
        public class MenuWidget
        {

        }
        internal List<SilkButton> menu_buttons = new();

        public void RegisterMenuItem(SilkButton button)
        {
            menu_buttons.Add(button);
        }

        public MainMenuRegistry()
        {

        }
    }

    public class VersionShowerRegistry
    {
        internal List<string> version_strings = new List<string>();

        public void RegisterVersionString(string version_string)
        {
            version_strings.Add(version_string);
        }

        public VersionShowerRegistry()
        {

        }
    }

    [HarmonyPatch(typeof(MenuButtonList), "SetupActive")]
    class MenuButtonListSetupPatch
    {
        static void Postfix(MenuButtonList __instance)
        {
            if (__instance.name == "MainMenuButtons")
            {

                foreach (SilkButton button in SilkApi.instance.Registries.UI.Menu.menu_buttons)
                {
                    var btn = PrefabRegistry.instance.instantiate_base_btn(__instance.transform);
                    
                    Utils.PatchButtonEvents(btn.GetComponent<MenuButton>(), button.Interaction);

                    var text = btn.transform.Find("Menu Button Text");

                    btn.name = button.Text.Trim() + "Button";

                    text.GetComponent<UnityEngine.UI.Text>().text = button.Text;

                    var selectable = btn.GetComponent<UnityEngine.UI.Selectable>();

                    __instance.activeSelectables.Add(selectable);

                    var count = __instance.activeSelectables.Count;
                    var btnIndex = count - 1;

                    Selectable selectOnUp = __instance.activeSelectables[(btnIndex + count - 1) % count];
                    Selectable selectOnDown = __instance.activeSelectables[(btnIndex + 1) % count];

                    // Controller/Keyboard navigation support
                    if (selectable.navigation.mode == Navigation.Mode.Explicit)
                    {
                        Navigation newNav = selectable.navigation;
                        newNav.selectOnUp = selectOnUp;
                        newNav.selectOnDown = selectOnDown;
                        selectable.navigation = newNav;

                        Navigation replaceUpNav = selectOnUp.navigation;
                        replaceUpNav.selectOnDown = selectable;
                        selectOnUp.navigation = replaceUpNav;

                        Navigation replaceDownNav = selectOnDown.navigation;
                        replaceDownNav.selectOnUp = selectable;
                        selectOnDown.navigation = replaceDownNav;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(SetVersionNumber), "Start")]
    class VersionShowerPatch
    {
        static void Postfix(SetVersionNumber __instance)
        {
            __instance.textUi.text += " | Modded";

            foreach (string version in SilkApi.instance.Registries.UI.VersionShower.version_strings)
            {
                __instance.textUi.text += $"\n{version}";
            }
        }
    }
}
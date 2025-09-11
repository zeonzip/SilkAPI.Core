using System.Collections.Generic;
using HarmonyLib;
using SilkAPIPlugin;
using UnityEngine.UI;

namespace SilkAPI.API.Registries
{
    public class UIRegistry
    {
        public MainMenuRegistry Menu = new MainMenuRegistry();
        public VersionShowerRegistry VersionShower = new VersionShowerRegistry();

        public UIRegistry()
        {

        }
    }

    public enum MenuType
    {
        MainMenu, PauseMenu
    }

    public class MainMenuRegistry
    {
        public class MenuWidget
        {

        }
        internal List<string> menu_buttons = new List<string>();

        public void RegisterMenuItem(string button_name)
        {
            menu_buttons.Add(button_name);
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

                var base_btn = __instance.transform.Find("StartGameButton").gameObject;

                foreach (string button_text in SilkApi.instance.Registries.UI.Menu.menu_buttons)
                {
                    var btn = UnityEngine.Object.Instantiate(base_btn, __instance.transform);
                    Utils.StripLocalizer(btn);

                    Utils.PatchButtonEvents(btn.GetComponent<MenuButton>(), (a) =>
                    {
                        SilkApiPlugin.ui_manager.ui_manager.UIGoToAchievementsMenu();
                    });

                    var text = btn.transform.Find("Menu Button Text");

                    btn.name = button_text.Trim() + "Button";

                    text.GetComponent<UnityEngine.UI.Text>().text = button_text;

                    var selectable = btn.GetComponent<UnityEngine.UI.Selectable>();

                    __instance.activeSelectables.Add(selectable);

                    var count = __instance.activeSelectables.Count;
                    var btn_index = count - 1;

                    Selectable selectOnUp = __instance.activeSelectables[(btn_index + count - 1) % count];
                    Selectable selectOnDown = __instance.activeSelectables[(btn_index + 1) % count];

                    SilkApiPlugin.Logger.LogInfo($"Linking {selectable.name} to {selectOnUp.name} and {selectOnDown.name}");

                    // Controller/Keyboard navigation support
                    if (selectable.navigation.mode == Navigation.Mode.Explicit)
                    {
                        SilkApiPlugin.Logger.LogInfo($"EXPLICIT Linking {selectable.name} to {selectOnUp.name} and {selectOnDown.name}");

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
using SilkAPI;
using SilkAPIPlugin;
using HarmonyLib;
using UnityEngine;
using GlobalEnums;

namespace SilkAPI
{
    internal class SilkUIManager
    {
        internal UIManager ui_manager;

        internal GameObject mods_screen;
        public enum SilkMainMenuState
        {
            LOGO,
            MAIN_MENU,
            OPTIONS_MENU,
            GAMEPAD_MENU,
            KEYBOARD_MENU,
            SAVE_PROFILES,
            AUDIO_MENU,
            VIDEO_MENU,
            EXIT_PROMPT,
            OVERSCAN_MENU,
            GAME_OPTIONS_MENU,
            ACHIEVEMENTS_MENU,
            QUIT_GAME_PROMPT,
            RESOLUTION_PROMPT,
            BRIGHTNESS_MENU,
            PAUSE_MENU,
            PLAY_MODE_MENU,
            EXTRAS_MENU,
            REMAP_GAMEPAD_MENU,
            EXTRAS_CONTENT_MENU,
            ENGAGE_MENU,
            // Additional menu items
            MODS_MENU
        }

        internal SilkMainMenuState menu_state;


        internal static SilkUIManager init(UIManager uiManager)
        {
            SilkUIManager silk_ui = new SilkUIManager();

            silk_ui.mods_screen = GameObject.Instantiate(uiManager.achievementsMenuScreen.gameObject, uiManager.gameObject.transform.Find("UICanvas"));
            silk_ui.ui_manager = uiManager;

            silk_ui.menu_state = SilkMainMenuState.LOGO;

            silk_ui.init_mods_screen();

            return silk_ui;
        }

        internal void init_mods_screen()
        {
            this.mods_screen.SetActive(false);
            this.mods_screen.name = "ModsMenuScreen";

            Transform title = this.mods_screen.transform.Find("Title");

            title.GetComponent<UnityEngine.UI.Text>().text = "Mods";
            Utils.StripLocalizer(title.gameObject);
        }
    }
}

[HarmonyPatch(typeof(UIManager), "Start")]
class UIManager_Start_Patch
{
    static void Postfix(UIManager __instance)
    {
        SilkApiPlugin.ui_manager = SilkUIManager.init(__instance);
    }
}

[HarmonyPatch(typeof(UIManager), "SetMenuState")]
class UIManager_SetState_Patch
{
    static void Postfix(MainMenuState newState, UIManager __instance)
    {
        SilkApiPlugin.ui_manager.menu_state = (SilkUIManager.SilkMainMenuState)newState;
    }
}
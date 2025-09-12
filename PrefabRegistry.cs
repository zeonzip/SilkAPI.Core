using UnityEngine;
using UnityEngine.UI;
using HarmonyLib;
using SilkAPI;
using SilkAPIPlugin;

namespace SilkAPI
{
    internal class PrefabRegistry : MonoBehaviour
    {
        public static PrefabRegistry instance;

        private MenuButton base_btn;

        public void BaseButton(MenuButton btn)
        {
            var obj = GameObject.Instantiate(btn.gameObject, this.gameObject.transform);
            obj.SetActive(false);
            Utils.StripLocalizer(obj);

            this.base_btn = obj.GetComponent<MenuButton>();
        }

        public MenuButton instantiate_base_btn(Transform parent)
        {
            var obj = GameObject.Instantiate(this.base_btn.gameObject, parent);
            obj.SetActive(true);

            return obj.GetComponent<MenuButton>();
        }
    }
}

[HarmonyPatch(typeof(GameManager), "Awake")]
class GameManager_Patch
{
    public static void Prefix(GameManager __instance)
    {
        GameObject obj_registry = new GameObject("SilkApi.Core_PrefabRegistry");
        obj_registry.AddComponent<PrefabRegistry>();
        GameObject.DontDestroyOnLoad(obj_registry);

        PrefabRegistry registry = obj_registry.GetComponent<PrefabRegistry>();
        PrefabRegistry.instance = registry;
    }
}

[HarmonyPatch(typeof(UIManager), "Start")]
class UIManager_Patch
{
    public static void Prefix(UIManager __instance)
    {
        PrefabRegistry.instance.BaseButton(__instance.mainMenuButtons.startButton);
    } 
}
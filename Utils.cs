using SilkAPIPlugin;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SilkAPI
{
    public class Utils
    {
        public static void StripLocalizer(GameObject obj)
        {
            var localizer = obj.GetComponent<AutoLocalizeTextUI>();
            UnityEngine.Object.Destroy(localizer);
        }

        public static void StripEventTrigger(GameObject obj)
        {
            var eventTrigger = obj.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            UnityEngine.Object.Destroy(eventTrigger);
        }

        public static void PatchButtonEvents(MenuButton button, UnityAction<BaseEventData> action)
        {
            SilkApiPlugin.Logger.LogInfo("Patching button events.");

            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            GameObject.Destroy(trigger);

            trigger = button.gameObject.AddComponent<EventTrigger>();

            // Click entry
            EventTrigger.Entry clickEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            clickEntry.callback.AddListener(action);
            trigger.triggers.Add(clickEntry);


            // Submit entry
            EventTrigger.Entry submitEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Submit
            };

            submitEntry.callback.AddListener(action);
            trigger.triggers.Add(submitEntry);
        }
    }
}

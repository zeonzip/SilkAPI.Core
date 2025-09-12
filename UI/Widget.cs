using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SilkAPI.UI;

public abstract class SilkWidget
{
    // TODO: add common functions
}

public class SilkButton : SilkWidget
{
    public string Text;
    public UnityAction<BaseEventData> Interaction;

    public SilkButton(string text, UnityAction<BaseEventData> interaction)
    {
        Text = text;
        Interaction = interaction;
    }
}
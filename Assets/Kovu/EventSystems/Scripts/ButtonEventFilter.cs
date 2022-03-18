using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Kovu.EventSystem
{
    public class ButtonEventFilter : EventFilter
    {
        public List<PointerEventData.InputButton> buttons;

        public override bool Filter(PointerEventData eventData)
        {
            return buttons.IndexOf(eventData.button) != -1;
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kovu.EventSystem
{
    public abstract class EventFilter : MonoBehaviour
    {
        public virtual bool Filter(PointerEventData eventData)
        {
            return Filter((BaseEventData)eventData);
        }

        public virtual bool Filter(AxisEventData eventData)
        {
            return Filter((BaseEventData)eventData);
        }

        public virtual bool Filter(BaseEventData eventData)
        {
            return true;
        }
    }
}
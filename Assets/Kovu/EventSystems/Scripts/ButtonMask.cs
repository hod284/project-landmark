using UnityEngine;
using UnityEngine.EventSystems;

namespace Kovu.EventSystems
{
    public class ButtonMask : MonoBehaviour
    {
        public int value = 0x0007; // Left | Right | Middle

        public bool Contains(PointerEventData.InputButton button)
        {
            return (value & (1 << (int)button)) != 0;
        }
    }
}
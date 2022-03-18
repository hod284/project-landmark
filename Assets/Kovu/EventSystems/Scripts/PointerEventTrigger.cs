using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Kovu.EventSystems
{
    public enum PointerEventTriggerType
    {
        PointerEnter = 0,
        PointerExit,
        PointerDown,
        PointerUp,
        PointerClick,
        PointerDoubleClick,
        Drag,
        Drop,
        Scroll,
        InitializePotentialDrag,
        BeginDrag,
        EndDrag
    }

    public class PointerEventTrigger : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler,
        IInitializePotentialDragHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler
    {
        public bool allowDragWhileClick = true;
        public bool enableDoubleClicks = false;

        [Serializable]
        public class TriggerEvent : UnityEvent<PointerEventData> { }

        [Serializable]
        public class Entry
        {
            public PointerEventTriggerType eventID = PointerEventTriggerType.PointerClick;
            public TriggerEvent callback = new TriggerEvent();
        }

        private float doubleClickTime
        {
            get
            {
                if (PointerControlPanel.current == null)
                    return 0;
                else
                    return PointerControlPanel.current.doubleClickTime;
            }
        }

        private class ButtonStatus
        {
            public float lastClickTime = -1000;
            public int doubleClickCount = 0;
            public bool dragWhileClick = false;
        }

        private DefaultDictionary<PointerEventData.InputButton, ButtonStatus> buttonStatusTable
            = new DefaultDictionary<PointerEventData.InputButton, ButtonStatus>(() => new ButtonStatus());

        [SerializeField]
        private List<Entry> m_Delegates;

        [SerializeField]
        private ButtonMask m_ButtonMask;

        public ButtonMask buttonMask
        {
            get { return m_ButtonMask; }
            set { m_ButtonMask = value; }
        }

        public List<Entry> triggers
        {
            get
            {
                if (m_Delegates == null)
                    m_Delegates = new List<Entry>();
                return m_Delegates;
            }
            set { m_Delegates = value; }
        }

        private void Execute(PointerEventTriggerType id, PointerEventData eventData)
        {
            if (m_ButtonMask != null && !m_ButtonMask.Contains(eventData.button))
                return;

            for (int i = 0, imax = triggers.Count; i < imax; ++i)
            {
                var ent = triggers[i];
                if (ent.eventID == id && ent.callback != null)
                    ent.callback.Invoke(eventData);
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.PointerEnter, eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.PointerExit, eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            buttonStatusTable[eventData.button].dragWhileClick = true;
            Execute(PointerEventTriggerType.Drag, eventData);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.Drop, eventData);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            buttonStatusTable[eventData.button].dragWhileClick = false;
            Execute(PointerEventTriggerType.PointerDown, eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.PointerUp, eventData);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            var button = eventData.button;
            var buttonStatus = buttonStatusTable[button];
            if (allowDragWhileClick || !buttonStatus.dragWhileClick)
            {
                var now = Time.realtimeSinceStartup;
                var elapsed = now - buttonStatus.lastClickTime;
                if (enableDoubleClicks && (elapsed < doubleClickTime))
                    buttonStatus.doubleClickCount = (buttonStatus.doubleClickCount + 1) % 2;
                else
                    buttonStatus.doubleClickCount = 0;

                if (buttonStatus.doubleClickCount == 0)
                    Execute(PointerEventTriggerType.PointerClick, eventData);
                else
                    Execute(PointerEventTriggerType.PointerDoubleClick, eventData);

                buttonStatus.lastClickTime = now;
                buttonStatus.dragWhileClick = false;
            }
        }

        public virtual void OnScroll(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.Scroll, eventData);
        }

        public virtual void OnInitializePotentialDrag(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.InitializePotentialDrag, eventData);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.BeginDrag, eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            Execute(PointerEventTriggerType.EndDrag, eventData);
        }
    }
}
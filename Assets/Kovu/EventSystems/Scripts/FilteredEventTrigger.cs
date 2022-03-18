using UnityEngine;
using UnityEngine.EventSystems;

namespace Kovu.EventSystem
{
    public class FilteredEventTrigger : EventTrigger
    {
        public bool allowDragWhileClick = true;
        public EventFilter filter;
        public bool enableDoubleClick = false;

        private float doubleClickTime = 0.5f;
        private float lastClickTime = -1000;
        private bool _dragWhileClick = false;
        private Vector3 prevPos = Vector3.zero;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnBeginDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnEndDrag(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            var delta = Camera.main.ScreenToViewportPoint(Input.mousePosition) - prevPos;
            if (delta.magnitude > 1 ||
                Input.GetKey(KeyCode.LeftControl))
                return;

            if (allowDragWhileClick || !_dragWhileClick)
            {
                if (filter == null || filter.Filter(eventData))
                {
                    var now = Time.realtimeSinceStartup;
                    var elapsed = now - lastClickTime;
                    if (enableDoubleClick && (elapsed < doubleClickTime))
                    {
                        base.OnPointerClick(eventData);
                    }
                    else if (!enableDoubleClick)
                    {
                        base.OnPointerClick(eventData);
                    }

                    lastClickTime = now;
                }
            }
            _dragWhileClick = false;
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnInitializePotentialDrag(eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnDrop(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            prevPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _dragWhileClick = false;
            if (filter == null || filter.Filter(eventData))
                base.OnPointerDown(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            _dragWhileClick = true;
            if (filter == null || filter.Filter(eventData))
                base.OnDrag(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnPointerExit(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnPointerUp(eventData);
        }

        public override void OnScroll(PointerEventData eventData)
        {
            if (filter == null || filter.Filter(eventData))
                base.OnScroll(eventData);
        }
    }
}
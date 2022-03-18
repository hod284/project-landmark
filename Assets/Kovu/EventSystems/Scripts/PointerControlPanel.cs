using System.Collections.Generic;
using UnityEngine;

namespace Kovu.EventSystems
{
    public class PointerControlPanel : MonoBehaviour
    {
        [Range(0.001f, 5)]
        public float doubleClickTime = 0.5f;

        private static List<PointerControlPanel> _allInstances = new List<PointerControlPanel>();

        public static PointerControlPanel current
        {
            get
            {
                if (_allInstances.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (_allInstances.Count > 1)
                        Debug.LogWarning("Multiple PointerControlPanel in scene.");
                    return _allInstances[0];
                }
            }
        }

        private void Awake()
        {
            _allInstances.Add(this);
        }

        private void OnDestroy()
        {
            _allInstances.Remove(this);
        }
    }
}
using UnityEngine;

namespace Kovu.CameraSystems
{
    [RequireComponent(typeof(CameraController))]
    public class Focuser : MonoBehaviour
    {
        public LayerMask eventMask;
        public float maxDistance = 10000;

        public void Focus(Vector3 mousePosition)
        {
            var controller = GetComponent<CameraController>();
            var ray = controller.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance))
                controller.FocusOn(hitInfo.point);
        }
    }
}
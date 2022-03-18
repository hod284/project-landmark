using System;
using System.Collections;
using UnityEngine;

namespace Kovu.CameraSystems
{
    public class CameraController : MonoBehaviour
    {
        public Transform rig;
        public float CurveDuration = 1f;
        public float minTransitionTime = 1;
        public float maxTransitionTime = 1;
        public float transitionSpeed = 1;
        public AnimationCurve focusCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float epsilon = Mathf.Epsilon;
        private IEnumerator _transition;

        public bool isMain = true;

        public Vector3 forward { get { return transform.forward; } }
        public Vector3 back { get { return -forward; } }
        public Vector3 right { get { return transform.right; } }
        public Vector3 left { get { return -right; } }
        public Vector3 up { get { return transform.up; } }
        public Vector3 down { get { return -up; } }

        public Camera camera{get; private set;}

        private void Awake()
        {
            if (rig == null)
                rig = transform;

            camera = GetComponent<Camera>();
            if (camera == null)
                Debug.LogWarning("Camera component is missing. This would cause a run-time error.");
        }
        /*
        public static CameraController GetCurrentController()
        {
            return PageManager.Instance.GetPage<PageScenario>().
        }
        */

        public void FocusOn(Vector3 position)
        {
            var cameraPosition = transform.position;
            var diff = position - cameraPosition;
            var x = Vector3.Dot(right, diff);
            var y = Vector3.Dot(up, diff);
            var delta = right * x + up * y;
            var from = transform.position;
            var to = transform.position + delta;
            StartTransition(DoFocusOn(from, to, transform.rotation, transform.rotation));
        }
        public void FocusOn(Vector3 position, Quaternion rotation)
        {
            var cameraPosition = transform.position;
            var diff = position - cameraPosition;
            var x = Vector3.Dot(right, diff);
            var y = Vector3.Dot(up, diff);
            var delta = right * x + up * y;
            var from = transform.position;
            var to = transform.position + delta;
            StartTransition(DoFocusOn(from, to, transform.rotation, rotation));
        }

        private void StartTransition(IEnumerator transition)
        {
            if (_transition != null)
                StopCoroutine(_transition);

            _transition = transition;
            StartCoroutine(transition);
        }

        internal void SitOn(Transform transform)
        {
            LookAt(transform.position, -transform.forward, 0);
        }

        internal void MoveWorld(Vector3 direction)
        {
            rig.position += direction;
        }

        private bool Approximately(float a, float b)
        {
            return Mathf.Abs(a - b) < epsilon;
        }

        public void Move(Transform target)
        {
            LookAt(target.position, target.forward * -1f, 0f);
        }

        public void LookAt(Transform target, float distance = 2f)
        {
            LookAt(target.position, target.forward, distance);
        }

        internal void LookAt(Vector3 point, Vector3 normal, float lookAtDistance)
        {
            var upwards = Vector3.up;
            var dot = Vector3.Dot(normal, upwards);
            if (Approximately(Math.Abs(dot), 1))
                upwards = Vector3.Cross(transform.right, normal);
            var newPosition = point + normal * lookAtDistance;
            var oldPosition = transform.position;
            var oldRotation = transform.rotation;
            var newRotation = Quaternion.LookRotation(-normal, upwards);
            StartTransition(DoFocusOn(oldPosition, newPosition, oldRotation, newRotation));
        }

        public void Rotate(Vector3 delta)
        {
            Rotate(delta.x, delta.y);
        }

        public void Rotate(float dx, float dy)
        {
            var camera = GetComponent<Camera>();
            var screenMax = camera.ViewportToScreenPoint(Vector3.one);
            var screenMin = camera.ViewportToScreenPoint(Vector3.zero);
            var ratio = camera.fieldOfView / (screenMax - screenMin).y;

            var pitch = Quaternion.AngleAxis(-dy * ratio, Vector3.right);
            var yaw = Quaternion.AngleAxis(dx * ratio, Vector3.up);
            rig.rotation = yaw * rig.rotation * pitch;
        }

        public void Zoom(float delta)
        {
            rig.position += forward * delta;
        }

        public void MoveBackLocal(float delta)
        {
            MoveForwardLocal(-delta);
        }

        public void MoveBack(float delta)
        {
            MoveForward(-delta);
        }

        public void MoveForward(float delta)
        {
            var forward = Vector3.Cross(transform.right, Vector3.up);
            rig.position += forward * delta;
        }

        public void MoveForwardLocal(float delta)
        {
            Zoom(delta);
        }

        public void MoveLeftLocal(float delta)
        {
            MoveRightLocal(-delta);
        }

        public void MoveRightLocal(float delta)
        {
            rig.position += transform.right * delta;
        }

        public void MoveUpWorld(float delta)
        {
            rig.position += Vector3.up * delta;
        }

        public void MoveUpLocal(float delta)
        {
            rig.position += transform.up * delta;
        }

        public void MoveLocal(Vector3 delta)
        {
            MoveRightLocal(delta.x);
            MoveUpLocal(delta.y);
            MoveForwardLocal(delta.z);
        }

        private Plane GetFocalPlane(float distance)
        {
            var forward = transform.forward;
            var point = transform.position + forward * distance;
            return new Plane(-forward, point);
        }

        private Vector3 RaycastFocalPlane(float distance, Ray ray)
        {
            float enter;
            bool hit = GetFocalPlane(distance).Raycast(ray, out enter);
            Debug.Assert(hit);
            return ray.GetPoint(enter);
        }

        public void MoveLocalScreenPoint(float distance, Vector3 last, Vector3 curr)
        {
            var camera = GetComponent<Camera>();
            var a = RaycastFocalPlane(distance, camera.ScreenPointToRay(last));
            var b = RaycastFocalPlane(distance, camera.ScreenPointToRay(curr));
            var diff = a - b;
            //정윤수 마우스휠로 카메라를 움직일때 오류로 여기서 막음
            // Debug.Assert(CloseEnough(Vector3.Dot(transform.forward, diff), 0));
            rig.position += diff;
        }

        public Ray GetCameraRay()
        {
            return new Ray(transform.position, transform.forward);
        }

        public Ray ScreenPointToRay(Vector3 position)
        {
            return GetComponent<Camera>().ScreenPointToRay(position);
        }

        public float GetTransitionTime(float distance)
        {
            return Mathf.Clamp(distance / transitionSpeed, minTransitionTime, maxTransitionTime);
        }

        private IEnumerator DoFocusOn(Vector3 oldPosition, Vector3 newPosition,
            Quaternion oldRotation, Quaternion newRotation)
        {
            var riggingDelta = transform.position - rig.position;
            var oldRigPosition = oldPosition - riggingDelta;
            var newRigPosition = newPosition - riggingDelta;
            float currTime = 0;
            var duration = GetTransitionTime((newPosition - oldPosition).magnitude);
            while (currTime < duration)
            {
                currTime += Time.deltaTime;
                var s = currTime / duration;
                var t = focusCurve.Evaluate(s);
                rig.position = Vector3.Lerp(oldRigPosition, newRigPosition, t);
                transform.rotation = Quaternion.Lerp(oldRotation, newRotation, t);
                yield return new WaitForEndOfFrame();
            }
        }

        private bool CloseEnough(float a, float b, float epsilon = Vector3.kEpsilon)
        {
            return Mathf.Abs(a - b) < epsilon;
        }
    }
}
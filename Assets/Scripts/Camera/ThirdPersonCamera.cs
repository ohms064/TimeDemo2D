using UnityEngine;

namespace Assets.Scripts.Camera {
    public class ThirdPersonCamera : MonoBehaviour {
        public Transform target;
        public float distance;
        public float height;
        public float duration;

        private float zPosition;
        private Vector3 _camPosition;
        private Ray camRay;
        private float currentDistance;

        // Update is called once per frame
        void Update () {
            var layer = 1 << 3;
            camRay.origin = this.transform.position;
            camRay.direction = target.position - camRay.origin;
            if(Physics.Raycast(camRay, currentDistance, layer)) {
                currentDistance -= 0.5f;
            }
            else if(currentDistance < distance) {
                currentDistance += 0.5f;
            }
            _camPosition = target.position - currentDistance * target.forward + Vector3.up * height;
            _camPosition.z = distance;
            this.transform.position = Vector3.Lerp(this.transform.position, _camPosition, Time.deltaTime * duration);
            this.transform.LookAt(target);
        }

#if UNITY_EDITOR
        void OnDrawGizmos() {
            if (Physics.Raycast(camRay)) Gizmos.color = Color.red;
            else Gizmos.color = Color.white;
            Gizmos.DrawRay(camRay);
        }
#endif
    }
}

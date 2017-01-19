using UnityEngine;

namespace Assets.Scripts.Enviroment_Scripts {
    public class InanimateUIController : MonoBehaviour {

        public Canvas arrow;

        void Start() {
            arrow.enabled = false;
        }

        public void Enable(Vector3 direction) {
            if ( arrow.enabled ) {
                return;
            }
            arrow.enabled = true;
            arrow.transform.up = -direction;
        }

        public void Disable() {
            if ( !arrow.enabled ) {
                return;
            }
            arrow.enabled = false;
        }

        public void Rotatate(Vector3 direction ) {
            if( arrow.enabled ) {
                arrow.transform.up = -direction;
            }
        }
    }
}

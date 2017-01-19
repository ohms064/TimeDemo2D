using UnityEngine;

namespace Assets.Scripts.Event_Scripts {
    public class Switch : MonoBehaviour {
        public SwitchEventListener[] affectedObjects;

        private Animator _animator;
        private bool isOn = false;
        private bool _isTransitioning = false;

        void Start() {
            _animator = GetComponent<Animator>();
        }

        public void TurnOn() {
            for(int i = 0; i < affectedObjects.Length; i++ ) {
                affectedObjects[i].onActivated(this);
            }
            isOn = true;
            _isTransitioning = false;
        }

        public void TurnOff() {
            for(int i = 0; i < affectedObjects.Length; i++ ) {
                affectedObjects[i].OnDeactivated(this);
            }
            isOn = false;
            _isTransitioning = false;
        }

        public void StartTransition() {
            _isTransitioning = true;
        }

        public void OnClicked() {
            if ( _isTransitioning )
                return;
            if (!_animator.GetBool( "first" ) ) {
                _animator.SetBool( "first", true );
                _animator.SetBool( "isOn", true );
                isOn = true;
            }else {
                isOn = !isOn;
                _animator.SetBool( "isOn", isOn );
            }

        }
    }
}

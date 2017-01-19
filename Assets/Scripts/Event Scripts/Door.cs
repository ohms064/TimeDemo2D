using UnityEngine;

namespace Assets.Scripts.Event_Scripts {
    public class Door : SwitchEventListener {

        private Animator _animator;
        public bool isOpenOnStart = true;

        // Use this for initialization
        void Start() {
            _animator = GetComponent<Animator>();
            _animator.SetBool( "isOpen", isOpenOnStart );
        }

        public override void onActivated( Switch fromWho ) {
            _animator.SetBool( "isOpen", !_animator.GetBool("isOpen" ));
        }

        public override void OnDeactivated( Switch fromWho ) {
            _animator.SetBool( "isOpen", !_animator.GetBool( "isOpen" ));
        }
    }
}

using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Controllers {
    public class PlayerAnimationController : MonoBehaviour {
        private Animator _playerAnimator;
        private float _movimiento;
        private bool _isRunning;
        // Use this for initialization
        void Start() {
            _playerAnimator = this.GetComponent<Animator>();
        }

        public void StartRunning() {
            _isRunning = true;
            _playerAnimator.SetBool( "Running", true );
        }

        public void StopRunning() {
            _isRunning = false;
            _playerAnimator.SetBool( "Running", false );
        }
    }
}

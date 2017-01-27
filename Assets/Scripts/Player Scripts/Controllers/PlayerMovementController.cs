using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Player_Scripts.Tools;
using UnityEngine;
using UnityStandardAssets._2D;

namespace Assets.Scripts.Player_Scripts.Controllers {
    [RequireComponent( typeof( Rigidbody2D ) )]
    public class PlayerMovementController : MonoBehaviour {
        private Rigidbody2D _rigidbody;
        private bool _isJumping;
        private bool _jumpButtonDown;
        private Ray _groundRay;
        private GroundedState _grounded;
        private float _movement;
        private const int JUMP_LAYER_MASK = 1531;
        private PlatformerCharacter2D _controller;

        public float runningThresold = 5.0f;
        public float runningMultiplier = 1.5f;
        public float jumpTime = 0.1f;
        public float movementSpeed = 5.0f;
        public float jumpForce = 5.0f;

        public bool FacingRight { get { return _controller.m_FacingRight; } }

        public Vector3 PlayerFront {
            get {
                return FacingRight ? this.transform.right : -this.transform.right;
            }
        } //Lo defino así porque puede que la transformación nunca cambie sino que se cambie por animación.

        // Use this for initialization
        private void Start() {
            _rigidbody = this.GetComponent<Rigidbody2D>();
            _groundRay = new Ray( this.transform.position - this.transform.localScale / 2, -this.transform.up );
            _controller = GetComponent<PlatformerCharacter2D>();
        }

        public void PushedTo(Vector3 direction) {
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }


#if UNITY_EDITOR

        private void OnDrawGizmos() {
            if ( Application.isPlaying ) {
                switch ( _grounded ) {
                    case GroundedState.GROUNDED:
                        Gizmos.color = Color.green;
                        break;
                    case GroundedState.SEMI_GROUNDED:
                        Gizmos.color = Color.magenta;
                        break;
                    case GroundedState.ON_AIR:
                        Gizmos.color = Color.red;
                        break;
                }
                Gizmos.DrawRay( _groundRay );
                Gizmos.color = Color.blue;
                Gizmos.DrawRay( this.transform.position, this.transform.right );
            }
        }

#endif
    }
}
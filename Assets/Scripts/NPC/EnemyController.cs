using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player_Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.NPC {
	[RequireComponent(typeof( Rigidbody2D ) )]
//Uses enum grounded state
	public class EnemyController : MonoBehaviour, IFreezable {
		[HideInInspector]public Rigidbody2D rb;
		public GroundedState groundedState;
		[HideInInspector]public bool isJumping;
		public float jumpForce;
		public float deltaDistance = 1.0f;
		public float walkingRayDistance = 2.0f;
		public float fallingRayDistance = 2.0f;
		public float wallRayDistance = 2.0f;

		private RaycastHit _walkingHit;
		private Vector3 _nextPosition;
		private Ray _fallingRay, _wallRay, _walkingRay;

		// Use this for initialization
		public void Start () {
			rb = GetComponent<Rigidbody2D>();
			_fallingRay = new Ray(Vector3.zero, Vector3.down);
		}
	
		private void Update() {
			if (groundedState != GroundedState.GROUNDED || this.rb.isKinematic) return;
			_walkingRay = new Ray(this.transform.position, -this.transform.up);
			if ( !Physics.Raycast( _walkingRay, out _walkingHit, walkingRayDistance ) ) return;
			Vector3 movementDirection = Vector3.ProjectOnPlane(this.transform.forward, _walkingHit.normal);


			_nextPosition = this.transform.position + (deltaDistance * movementDirection);
			_fallingRay = new Ray(_nextPosition, Vector3.down);
			_wallRay = new Ray(this.transform.position, this.transform.forward);
			if (!Physics.Raycast(_fallingRay, out _walkingHit, fallingRayDistance) || Physics.Raycast(_wallRay, wallRayDistance, 1535)) {
				Rotate();
			}
			else {
				rb.MovePosition(_nextPosition);
			}
		}

		public void Jump(float delta ) {
			switch ( groundedState ) {
				case GroundedState.GROUNDED:
					rb.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
					groundedState = GroundedState.ON_AIR;
					break;
				case GroundedState.ON_AIR:
					break;
				case GroundedState.SEMI_GROUNDED:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Rotate() {
			this.transform.forward = -this.transform.forward;
		}

		private void OnCollisionEnter2D( Collision2D coll) {
			if (coll.transform.CompareTag("Puzzle Cube")) {
				Rotate();
			}
		}

		public void FrozenRotation(float rotation) {
			return;
		}

		public virtual void Freeze() {
			this.rb.isKinematic = true;
			GetComponent<Renderer>().material.color = Color.cyan;
		}

		public virtual void Unfreeze() {
			this.rb.isKinematic = false;
			GetComponent<Renderer>().material.color = Color.red;
		}


#if UNITY_EDITOR
		void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(_walkingRay);
		}
#endif
	}
}

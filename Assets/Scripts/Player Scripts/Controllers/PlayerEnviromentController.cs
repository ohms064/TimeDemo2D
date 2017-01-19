using System.Collections;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using Assets.Scripts.Player_Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Controllers {
	public class PlayerEnviromentController : MonoBehaviour {
		public float rotationSensitivity = 2.0f;
		public float grabRange = 5.0f;
		public float objectsDamage = 1.0f;

		[HideInInspector]
		public GrabStatus grabStatus;
		[HideInInspector]
		public Transform grabedObject;

		private Ray _mouseRay;
		private RaycastHit2D _mouseHit;

		// Use this for initialization
		void Start () {
			grabStatus = GrabStatus.HANDS_FREE;
		}
	
		// Update is called once per frame
		void Update () {
			if ( Input.GetMouseButtonDown( 0 ) ) {
				if( grabStatus == GrabStatus.GRABING_OBJECT && !TimeManager.isFrozen) {
					Drop();
					return;
				}
				_mouseRay = UnityEngine.Camera.main.ScreenPointToRay( Input.mousePosition );

				_mouseHit = Physics2D.Raycast( _mouseRay.origin, _mouseRay.direction, Mathf.Infinity, 1024 );

				if ( _mouseHit.collider != null ) {
					print(_mouseHit.transform.gameObject.layer);
				
				
					grabedObject = _mouseHit.transform;
					if ( TimeManager.isFrozen && grabStatus != GrabStatus.ROTATING_OBJECT ) {
						grabStatus = GrabStatus.ROTATING_OBJECT;
						StartCoroutine( "RotateFrozenObject" );
						return;
					}
					if ( Vector3.Distance( grabedObject.position, this.transform.position ) < grabRange ) {
						if ( _mouseHit.transform.CompareTag( "Puzzle Cube" ) ) {
							Grab();
						}
						else if ( _mouseHit.transform.CompareTag( "Switch" ) ) {
							_mouseHit.transform.SendMessage( "OnClicked" );
						}
					}
					else {
						grabedObject = null;
						grabStatus = GrabStatus.HANDS_FREE;
					}
				}
			}
		}

		void Drop() {
			grabedObject.parent = null;
			grabedObject.GetComponent<Rigidbody2D>().isKinematic = false;
			grabedObject = null;
			grabStatus = GrabStatus.HANDS_FREE;
		}

		void Grab() {
			grabStatus = GrabStatus.GRABING_OBJECT;
			grabedObject.position = this.transform.position + ( PlayerManager.movement.PlayerFront );
			grabedObject.parent = this.transform;
			grabedObject.up = Vector3.up;
			grabedObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}

		IEnumerator RotateFrozenObject() {
			IFreezable frozenObject = grabedObject.GetComponent<IFreezable>();
			Vector3 mouseStartPosition = Input.mousePosition;
			Vector3 mouseVector;
			while ( Input.GetMouseButton( 0 ) ) { //Limitante de distancia && Vector3.Distance(this.transform.position, grabedObject.position) < grabRange
				mouseVector = Input.mousePosition - mouseStartPosition;
				frozenObject.FrozenRotation((mouseVector.x + mouseVector.y) * -rotationSensitivity);
				yield return new WaitForEndOfFrame();
			}
			grabStatus = GrabStatus.HANDS_FREE;
		}
	}
}

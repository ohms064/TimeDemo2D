using System.Collections;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Event_Scripts {
	public class FirstFreezeTime : MonoBehaviour {

		private bool hasExcecuted = false;
		public Rigidbody2D puzzleCube;
		public float time;

		void OnTriggerEnter2D( Collider2D col ) {
			if ( hasExcecuted )
				return;
			if ( col.transform.CompareTag( "Player" ) ) {
				hasExcecuted = true;
				StartCoroutine( "StartEvent" );
			}
		}

		IEnumerator StartEvent() {
			TimeManager.FreezeScene( false );
			PlayerManager.time.enabled = false;
			PlayerManager.animator.StopRunning();
			PlayerManager.movement.enabled = false;
			PlayerManager.time.Unfreeze();
			yield return new WaitForSeconds( time );
			PlayerManager.time.enabled = true;
			PlayerManager.movement.enabled = true;
			PlayerManager.time.Freeze();
		}
	
	}
}

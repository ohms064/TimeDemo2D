using System.Collections;
using Assets.Scripts.Base_Classes;
using Assets.Scripts.JSON;
using Assets.Scripts.Managers;
using Assets.Scripts.Player_Scripts.Skills;
using Assets.Scripts.Player_Scripts.Tools;
using Assets.Scripts.ProgressBars.Scripts;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Controllers {
	public class PlayerTimeController : MonoBehaviour {
		private TimeMaster spells;
		private FreezeJson freezeData;
		private float frozenTime;
		public ProgressBar frozenBar;


		// Use this for initialization
		void Start () {
			spells = new TimeMaster( new FreezeTimeSkill(), new Skill(), new Skill(), new Skill() );
			if ( FileManager.instance != null ) {
				frozenTime = FileManager.instance.currentFreezeJson.freezeTime;
			}
			else {
				frozenTime = 5.0f;
			}
		}
	
		// Update is called once per frame
		void Update () {
			if ( PlayerManager.enviroment.grabStatus != GrabStatus.HANDS_FREE )
				return;
			if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) {
				if (TimeManager.isFrozen) {
					Unfreeze();
				}
				else {
					Freeze();
				}
			}
			if ( Input.GetKeyDown( KeyCode.Alpha2 ) ) {
				spells.Haste.Activate();
			}
			if ( Input.GetKeyDown( KeyCode.Alpha3 ) ) {
				spells.Slow.Activate();
			}
			if ( Input.GetKeyDown( KeyCode.Alpha4 ) ) {
				spells.Rewind.Start();
			}
		}

		IEnumerator FreezeDuration() {
			float initTime = Time.time;
			while (Time.time - initTime < frozenTime) {
				yield return new WaitForEndOfFrame();
				frozenBar.SetPercentage(Mathf.InverseLerp( frozenTime, 0.0f, Time.time - initTime));
			}
			if (TimeManager.isFrozen) {
				spells.Freeze.Deactivate();
			}
		}

		public void Freeze() {
			spells.Freeze.Activate();
			StartCoroutine( "FreezeDuration" );
		}

		public void Unfreeze() {
			spells.Freeze.Deactivate();
			StopCoroutine( "FreezeDuration" );
		}
	}
}

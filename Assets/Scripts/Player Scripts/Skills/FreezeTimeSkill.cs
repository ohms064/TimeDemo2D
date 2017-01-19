using Assets.Scripts.Base_Classes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Skills {
    public class  FreezeTimeSkill : Skill  {

        public FreezeTimeSkill() {

        }

        public override void Activate() {
            TimeManager.FreezeScene(true);
        }

        public override void Deactivate() {
            TimeManager.FreezeScene(false);
        }
        public override void Start() {
            Debug.Log( "Not available" );
        }
    }
}

using UnityEngine;

namespace Assets.Scripts.Base_Classes {
    public class Skill {
        public virtual void Activate() {
            Debug.Log( "Not yet learned" );
        }

        public virtual void Deactivate() {
            Debug.Log( "Not yet learned" );
        }

        public virtual void Start() {
            Debug.Log( "Not yet learned" );
        }
    }
}
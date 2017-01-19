using Assets.Scripts.Interfaces;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers {
    public class TimeManager : MonoBehaviour {
        public static IFreezable[] timeBoundObjects;
        public static bool isRecording;
        public static bool isFrozen;

        // Use this for initialization
        void Start () {
            isRecording = false;
            timeBoundObjects = InterfaceHelper.FindObjects<IFreezable>();
        }

        public static void FreezeScene( bool state ) {
            if ( isFrozen == state )
                return;
            isFrozen = state;
            if ( isFrozen ) {
                foreach ( IFreezable obj in timeBoundObjects ) {
                    obj.Freeze();
                }
            }
            else {
                foreach ( IFreezable obj in timeBoundObjects ) {
                    obj.Unfreeze();
                }
            }
        }

        public static void StartRecording() {
            isRecording = true;
        }

    }
}

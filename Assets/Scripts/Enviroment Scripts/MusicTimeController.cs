using System.Collections;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enviroment_Scripts {
    public class MusicTimeController : MonoBehaviour, IFreezable {

        private AudioSource _source;
        public float freezePitch = 0.5f;
        public float slowTime = 1.0f;

        // Use this for initialization
        void Start () {
            _source = GetComponent<AudioSource>();
            slowTime = 1 / slowTime;
        }

        public void Freeze() {
            StopCoroutine( "Resume" );
            StartCoroutine( "Slower" );
        }

        public void FrozenRotation( float rotation ) {
            return;
        }

        public void Unfreeze() {
            StopCoroutine( "Slower" );
            StartCoroutine( "Resume" );
        }

        IEnumerator Slower() {
            float pitch = _source.pitch;
            float initTime = Time.time;
            while(pitch > freezePitch ) {
                _source.pitch = Mathf.Lerp( 1.0f, freezePitch, (Time.time - initTime) * slowTime );
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator Resume() {
            float pitch = _source.pitch;
            float initTime = Time.time;
            while ( pitch < 1.0f ) {
                _source.pitch = Mathf.Lerp( freezePitch, 1.0f, (Time.time - initTime) * slowTime );
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

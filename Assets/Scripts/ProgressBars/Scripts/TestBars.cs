using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ProgressBars.Scripts {
    public class TestBars : MonoBehaviour {

        public ProgressBar progressBar1, progressBar2;
        public float progressTime = 10.0f;
	
        // Update is called once per frame
        void Start () {
            StartCoroutine( "goUp" );
        }

        IEnumerator goUp() {
            float initTime = Time.time;
            float timePassed;
            Debug.Log( "goUp" );
            do {
                timePassed = (Time.time - initTime) / progressTime;
                progressBar1.SetPercentage( Mathf.Lerp( 0.0f, 1.0f, timePassed ) );
                progressBar2.SetPercentage( Mathf.Lerp( 0.0f, 1.0f, timePassed ) );
                yield return new WaitForEndOfFrame();
            } while ( timePassed < 1.0f );
            yield return new WaitForSeconds(1.0f);
            StartCoroutine( "goDown" );
        }
        IEnumerator goDown() {
            float initTime = Time.time;
            float timePassed;
            Debug.Log( "goDown" );
            do {
                timePassed = (Time.time - initTime) / progressTime;
                progressBar1.SetPercentage( Mathf.Lerp( 1.0f, 0.0f, timePassed ) );
                progressBar2.SetPercentage( Mathf.Lerp( 1.0f, 0.0f, timePassed ) );
                yield return new WaitForEndOfFrame();
            } while ( timePassed < 1.0f );
            yield return new WaitForSeconds( 1.0f );
            StartCoroutine( "goUp" );

        }
    }
}

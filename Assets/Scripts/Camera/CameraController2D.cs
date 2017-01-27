using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class CameraController2D : MonoBehaviour {

	public Transform target;
	public Vector2 offset;
	public float maxVelocity;
	public float distanceThreshold;
    [Range( 0f, 0.5f )]
    public float minDistance;

    private CameraState _camState;
    private float distance;

	void Start() {
        _camState = CameraState.RESTING;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetPos = target.position;
		Vector2 camPos = transform.position;

	    Vector2 realOffset = offset;
	    if ( !PlayerManager.movement.FacingRight ) { realOffset.x = -realOffset.x; }

		distance = Vector2.Distance( targetPos + realOffset, camPos );
        switch (_camState) {
            case CameraState.FOLLOWING:
                camPos = Vector2.Lerp( camPos, targetPos + realOffset, Time.deltaTime * maxVelocity );
                transform.position = new Vector3( camPos.x, camPos.y, transform.position.z );
                if ( distance < minDistance ) {
                    _camState = CameraState.RESTING;
                }
                break;
            case CameraState.RESTING:
                if ( distance > distanceThreshold) {
                    _camState = CameraState.FOLLOWING;
                }
                break;
        }
        
	}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if ( target == null ) {
            return;
        }

        Vector2 realOffset = offset;
        if ( Application.isPlaying && !PlayerManager.movement.FacingRight ) { realOffset.x = -realOffset.x; }

        Vector3 targetPosOff = new Vector3(target.position.x + realOffset.x, target.position.y + realOffset.y, target.position.z);
        if ( Vector3.Distance( targetPosOff, transform.position ) > distanceThreshold ) {
            Gizmos.color = Color.green;
        }
        else {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine( transform.position,  targetPosOff);
        Gizmos.color = Color.blue;
        Gizmos.DrawIcon( targetPosOff, "ikGoal" );
    }
#endif
}

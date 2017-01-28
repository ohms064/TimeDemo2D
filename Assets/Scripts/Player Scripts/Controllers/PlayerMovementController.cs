using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Player_Scripts.Tools;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent( typeof( Rigidbody2D ) )]
public class PlayerMovementController : MonoBehaviour {
    public Rigidbody2D playerRigidbody2D;
    private Animator _animator;
    private bool _jumpButtonDown;
    private GroundedState _grounded;
    private const int JUMP_LAYER_MASK = 1280;
    private const float RADIUS_THRESHOLD = 0.2f;

    public float runningThresold = 5.0f;
    public float runningMultiplier = 1.5f;
    public float jumpTime = 0.1f;
    public float movementSpeed = 5.0f;
    public float jumpForce = 5.0f;
    [Range(0f, 1f)]
    public float semiGroundedMultiplier = 0.75f;

    public bool isFacingRight = true;

    public Vector3 PlayerFront {
        get {
            return isFacingRight ? this.transform.right : -this.transform.right;
        }
    }

    // Use this for initialization
    private void Start() {
        playerRigidbody2D = this.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void PushedTo( Vector3 direction ) {
        playerRigidbody2D.AddForce( direction, ForceMode2D.Impulse );
    }

    private void FixedUpdate() {
        Collider2D[] colls = Physics2D.OverlapCircleAll( transform.position, RADIUS_THRESHOLD, JUMP_LAYER_MASK );
        if ( colls.Length <= 0 ) {
            _grounded = GroundedState.ON_AIR;
            _animator.SetBool( "Grounded", false );
        }
        else {
            for ( int i = 0; i < colls.Length; i++ ) {
                if ( colls[i].transform.tag.Equals( "Ground" ) ) {
                    _grounded = GroundedState.GROUNDED;
                    _animator.SetBool( "Grounded", true );
                    break;
                }
                else if ( colls[i].transform.tag.Equals( "Puzzle Cube" ) ) {
                    _grounded = GroundedState.SEMI_GROUNDED;
                    _animator.SetBool( "Grounded", true );
                }
            }
        }
        _animator.SetFloat( "vSpeed", playerRigidbody2D.velocity.y );


    }

    public void Move( float movement, bool jump, bool crouch ) {

        _animator.SetBool( "Crouching", crouch );

        _animator.SetFloat( "Speed", Mathf.Abs( movement ) );
        if ( !crouch ) {
            playerRigidbody2D.velocity = new Vector2( movement*movementSpeed, playerRigidbody2D.velocity.y );
        }
        if ( (movement > 0 && !isFacingRight) || (movement < 0 && isFacingRight) ) { Flip(); }

        _jumpButtonDown = jump;
        if ( jump && _grounded != GroundedState.ON_AIR ) {
            _grounded = GroundedState.ON_AIR;
            StartCoroutine( "JumpCoroutine", jumpForce * ( _grounded == GroundedState.GROUNDED ? 1 : semiGroundedMultiplier) );
            _animator.SetBool( "Grounded", false );
        }


    }

    private IEnumerator JumpCoroutine( float force ) {
        playerRigidbody2D.velocity = Vector2.zero;
        float timer = 0.0f;
        while ( _jumpButtonDown && timer < jumpTime ) {
            float jumpPercentage = timer / jumpTime;
            Vector2 currentVelocity = playerRigidbody2D.velocity;
            currentVelocity.y = Mathf.SmoothStep( force, 0, jumpPercentage );
            playerRigidbody2D.velocity = currentVelocity;
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        print( "JumpEnd" );
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos() {

        switch ( _grounded ) {
            case GroundedState.GROUNDED:
                Gizmos.color = Color.green;
                break;
            case GroundedState.SEMI_GROUNDED:
                Gizmos.color = Color.yellow;
                break;
            case GroundedState.ON_AIR:
                Gizmos.color = Color.red;
                break;
        }

        Gizmos.DrawWireSphere( transform.position, RADIUS_THRESHOLD );
        Gizmos.color = Color.blue;
        Gizmos.DrawRay( this.transform.position, this.transform.right );

    }


#endif
}

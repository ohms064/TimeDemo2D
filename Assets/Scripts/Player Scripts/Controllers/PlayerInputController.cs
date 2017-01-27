using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerInputController : MonoBehaviour {

    private PlayerMovementController _controller;
    private bool _isJumpPressed;

    // Use this for initialization
    void Start () {
	    _controller = GetComponent<PlayerMovementController>();
	}
	
	// Update is called once per frame
	void Update () {
	    _isJumpPressed = Input.GetKey( KeyCode.Space );
	}

    private void FixedUpdate() {
        float xMove = Input.GetAxis( "Horizontal" );
        _controller.Move( xMove, _isJumpPressed );
    }
}

using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Interfaces;

public class BasicInanimateTimeController : MonoBehaviour, IFreezable {

    private Rigidbody2D _rigidbody;
    private float _torqueAtStop;

    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Freeze() {
        _torqueAtStop = _rigidbody.angularVelocity;
        _rigidbody.isKinematic = true;
    }

    public void FrozenRotation( float rotation ) {
        Debug.Log( "Not available" );
    }

    public void Unfreeze() {
        _rigidbody.angularVelocity = _torqueAtStop;
        _rigidbody.isKinematic = false;
    }
}

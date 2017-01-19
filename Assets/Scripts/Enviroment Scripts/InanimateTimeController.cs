using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Enviroment_Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
// ReSharper disable All

[RequireComponent((typeof(Rigidbody2D)))]
public class InanimateTimeController : MonoBehaviour, IFreezable {
    private Rigidbody2D _rigidbody;
    private Vector2 _directionAtStop;
    private float _angularVelocityAtStop;
    private float _speedAtStop;
    //private PhysicMaterial _phsxMat;
    private float _forceUpAngle;
    [HideInInspector] public bool isHarmful = false;
    private float damage;
    private InanimateUIController _uiController;

    private Color _originalColor;

    // Use this for initialization
    void Start() {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _originalColor = this.GetComponent<Renderer>().material.color;
        _uiController = GetComponent<InanimateUIController>();
        //_phsxMat = this.GetComponent<Collider2D>().material;
    }

    public void FrozenRotation(float rotation) {
        this.transform.localEulerAngles = new Vector3( 0.0f, 0.0f, rotation );
        Quaternion quat = Quaternion.AngleAxis( _forceUpAngle, this.transform.forward );
        _directionAtStop = quat * this.transform.up;
        _uiController.Rotatate( _directionAtStop );
    }

    public void Freeze() {
        if (isHarmful) {
            isHarmful = false;
            StopCoroutine("ActiveDamage");
        }
        _directionAtStop = this._rigidbody.velocity;
        _speedAtStop = _directionAtStop.magnitude;
        if (Mathf.Abs(_speedAtStop) > 0.1) {
            _directionAtStop /= _speedAtStop;
            _uiController.Enable( _directionAtStop );
        }
        _angularVelocityAtStop = this._rigidbody.angularVelocity;
        _forceUpAngle = Vector3.Angle(this.transform.up, _directionAtStop);
        //_phsxMat.dynamicFriction = 0.0f; //Para que el personaje no se pegue a el.
        //_phsxMat.staticFriction = 0.0f;
        _rigidbody.isKinematic = true;
        _rigidbody.useFullKinematicContacts = true;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    public void Unfreeze() {
        this._rigidbody.isKinematic = false;
        _rigidbody.useFullKinematicContacts = false;
        if ( Mathf.Abs(_speedAtStop) > 0.0f) {
            this._rigidbody.velocity = _directionAtStop*_speedAtStop;
            this._rigidbody.angularVelocity = _angularVelocityAtStop;
        }
        
        if (Mathf.Abs(_speedAtStop) > 0.5f) {
            StartCoroutine("ActiveDamage");
        }
        else {
            GetComponent<Renderer>().material.color = _originalColor;
        }

        _uiController.Disable();
        //_phsxMat.dynamicFriction = 1.0f;
        //_phsxMat.staticFriction = 1.0f;
    }

    void OnCollisionEnter2D( Collision2D coll ) {
        if (coll.transform.CompareTag("Enviroment")) {
            isHarmful = false;
        }
    }

    IEnumerator ActiveDamage() {
        isHarmful = true;
        Renderer ren = GetComponent<Renderer>();
        ren.material.color = Color.red;
        damage = PlayerManager.enviroment.objectsDamage;
        while(damage > 0.0f || isHarmful) {
            yield return new WaitForFixedUpdate();
            damage = Mathf.InverseLerp(0.0f, _speedAtStop, _rigidbody.velocity.magnitude);
        }
        ren.material.color = _originalColor;
        isHarmful = false;

    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if ( TimeManager.isFrozen ) {
            Gizmos.color = new Color32( 106, 206, 240, 255 );
            Gizmos.DrawWireSphere( this.transform.position, 1.0f );
            Gizmos.color = Color.blue;
            Gizmos.DrawLine( this.transform.position, this.transform.position + new Vector3( _directionAtStop.x, _directionAtStop .y) );
        }
        else {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere( this.transform.position, 1.0f );
        }
    }

    
#endif

}

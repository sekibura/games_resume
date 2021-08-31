using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchimedForceScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _force = 15f;
    private void Start()
    {
        _rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(_rb == null)
            _rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        else
            _rb.AddForce(Vector3.up* _force, ForceMode2D.Force);
    }

}

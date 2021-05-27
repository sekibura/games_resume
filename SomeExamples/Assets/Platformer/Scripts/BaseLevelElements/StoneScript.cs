using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : Attackable
{
    private Rigidbody2D _rb;
    private float _prevVelocity = 0;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        int direction = playerPosition.x > transform.position.x ? -1 : 1;
        _rb.AddForce(new Vector2(150*direction,100), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject+" "+Mathf.Round(_rb.velocity.magnitude));
        
        if (_rb.velocity.magnitude > 1 && !collision.transform.CompareTag("Player"))
            collision.gameObject.GetComponent<Attackable>()?.ApplyDamage(100, transform.position);
    }



}

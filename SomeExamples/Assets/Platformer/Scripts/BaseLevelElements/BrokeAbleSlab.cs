using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeAbleSlab : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _states;
    private int _spriteNumber = 0;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _velocityValueToBrokePlayer;
    [SerializeField]
    private float _forceValueToBrokeRB;
    [SerializeField]
    private ParticleSystem _particle;

    private Rigidbody2D _rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Vector2 velocity = collision.gameObject.GetComponent<PhysicObject>().GetVelocity();
            if (velocity.y >= _velocityValueToBrokePlayer)
                ToBroke();
        }
        else
        {
            _rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (_rb != null)
                if (_rb.velocity.y * _rb.mass >= _forceValueToBrokeRB)
                    ToBroke();
        }
    }

    private void ToBroke()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeAbleSlab : Attackable
{
    [SerializeField]
    private Sprite[] _statesSprites;
    private int _currentSpriteNumber = 0;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _velocityValueToBrokePlayer;
    [SerializeField]
    private float _forceValueToBrokeRB;
    
    private ParticleSystem _particle;

    private Rigidbody2D _rb;

    private float _lastTime;
    private BoxCollider2D _collider;

    

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _statesSprites[_currentSpriteNumber];
        _lastTime = Time.time;

        _particle = gameObject.GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //if (Time.time < _lastTime + 0.5f)
        //    return;

        //if(collision.transform.tag == "Player")
        //{
        //    Vector2 velocity = collision.gameObject.GetComponent<PhysicObject>().GetVelocity();
        //    Debug.Log(velocity.y+" "+ collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        //    if (Mathf.Abs(velocity.y) >= _velocityValueToBrokePlayer)
        //        ToBroke();

        //}
        //else
        //{
        //    Debug.Log(_rb.velocity.y * _rb.mass);
        //    _rb = collision.gameObject.GetComponent<Rigidbody2D>();
        //    if (_rb != null)
        //        if (Mathf.Abs(_rb.velocity.y * _rb.mass) >= _forceValueToBrokeRB)
        //            ToBroke();
        //}
    }

    private void ToBroke()
    {
        _particle.Play();
        if (_currentSpriteNumber < _statesSprites.Length - 1)
        {
            _currentSpriteNumber++;
            _spriteRenderer.sprite = _statesSprites[_currentSpriteNumber];
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            Random.seed = System.DateTime.Now.Millisecond;
            _rb.AddTorque(Random.Range(-1f, 1f), ForceMode2D.Impulse);
            _collider.enabled = false;
            Destroy(gameObject,0.5f);
        }
    }

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        ToBroke();
    }

    public override int GetHp()
    {
        return 0;
    }
}

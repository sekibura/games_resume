using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMushroom : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _forcePlayer;
    [SerializeField]
    private float _forceRB;

    private float _delay= 0.5f;
    private float _lastTime;
    private Vector2 sizeSprite;

    private PlayerController _playerController;
    private void Start()
    {
        _lastTime = Time.time;
        _playerController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        sizeSprite = gameObject.GetComponent<SpriteRenderer>().size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_lastTime + _delay >= Time.time)
            return;

        _lastTime = Time.time;
      
            
        Vector2 position = collision.gameObject.transform.position;
        //Debug.Log(position+" |"+ gameObject.transform.position.y + sizeSprite.y / 2+"|"+ (gameObject.transform.position.x - sizeSprite.x / 2).ToString()+"|"+(gameObject.transform.position.x + sizeSprite.x / 2).ToString());
        if (position.y >= gameObject.transform.position.y+ sizeSprite.y/2 && position.x>=gameObject.transform.position.x-sizeSprite.x/2 && position.x <= gameObject.transform.position.x + sizeSprite.x / 2)
        {
            if (collision.gameObject.tag != "Player")
            {
                _rb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (_rb != null)
                {
                    Debug.Log("rb impulse "+_forceRB);
                    _rb.AddForce(new Vector3(0, 1, 0) * _forceRB, ForceMode2D.Impulse);
                }
                _rb = null;
            }
            else
            {
                _playerController.AddJumpImpulse(_forcePlayer);
            }
        }
            
     
        
    }
}

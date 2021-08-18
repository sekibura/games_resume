using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : Attackable
{
    [SerializeField]
    private int _maxHp;
    private int _currentHp;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private HealthBar _healthBar;
    private GameStateScript _gameStateManager;
    private PlayerController _controller;

    private float _deltaDamageTime = 1f;
    private float _lastDamageTime;
 

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
        _currentHp = _maxHp;
        _animator = gameObject.GetComponent<Animator>();
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        _healthBar.Init(_maxHp);
        _controller = GetComponent<PlayerController>();

        _gameStateManager = GameObject.Find("UI").GetComponent<GameStateScript>();
    }

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        if (Time.time>_lastDamageTime+_deltaDamageTime)
        {
            _lastDamageTime = Time.time;
            _controller.Attacked(playerPosition);
            Debug.Log(this.name+" - i was damaged! == "+damageValue);
            _currentHp -= damageValue;
        
            _healthBar.ApplyDamage(damageValue);
            _animator.SetTrigger("GetDamage");
            if (_currentHp <= 0)
                toDie();
            AudioManager.Instance.Play("PlayerDamage");
        }
        
    }

    private void toDie()
    {
        Debug.Log(gameObject.name+" - its time to die...");
        _collider.enabled = false;
        _rb.gravityScale = 0;        
        _animator.SetTrigger("Dead");
        _gameStateManager.GameOver(true);
        ChangeAllChildColliders(false);
    }
    public void Alive()
    {
        _animator.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _animator.gameObject.GetComponent<Collider2D>().enabled = true;
        _collider.enabled = true;
        _rb.gravityScale = 1;
        _animator.Play("player_idle");
        _gameStateManager.GameOver(false);
        ChangeAllChildColliders(true);
        AddHp(_maxHp);
    }

    public void AddHp(int value)
    {
        _currentHp += value;
        AudioManager.Instance.Play("AddHp");
        if (_currentHp > _maxHp)
            _currentHp = _maxHp;
        _healthBar.SetHealth(_currentHp);
    }

    private void ChangeAllChildColliders(bool value)
    {
        Collider2D[] col = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childCollider in col)
        {
            childCollider.enabled = value;
        }
    }

}

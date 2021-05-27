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
 

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
        _currentHp = _maxHp;
        _animator = gameObject.GetComponent<Animator>();
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        _healthBar.Init(_maxHp);
        _controller = GetComponent<PlayerController>();

        _gameStateManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateScript>();
    }

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        _controller.Attacked(playerPosition);
        Debug.Log(this.name+" - i was damaged!");
        _currentHp -= damageValue;
        
        _healthBar.ApplyDamage(damageValue);
        _animator.SetTrigger("GetDamage");
        if (_currentHp <= 0)
            toDie();

        AudioManager.Instance.Play("PlayerDamage");
    }

    private void toDie()
    {
        Debug.Log("its time to die...");
        _collider.enabled = false;
        _rb.gravityScale = 0;        
        _animator.SetTrigger("Dead");
        _gameStateManager.GameOver();
        DisableAllChildColliders();
    }

    public void AddHp(int value)
    {
        _currentHp += value;
        AudioManager.Instance.Play("AddHp");
        _healthBar.IncreaseHealth(value);
        if (_currentHp > _maxHp)
            _currentHp = _maxHp;
    }

    private void DisableAllChildColliders()
    {
        Collider2D[] col = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childCollider in col)
        {
            childCollider.enabled = false;
        }
    }

}

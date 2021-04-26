using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHp;
    private int _currentHp;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private HealthBar _healthBar;
 

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
        _currentHp = _maxHp;
        _animator = gameObject.GetComponent<Animator>();
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        _healthBar.Init(_maxHp);
    }

   public void ApplyDamage(int damageValue)
    {
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

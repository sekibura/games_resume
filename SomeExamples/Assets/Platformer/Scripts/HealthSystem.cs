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
 

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
        _currentHp = _maxHp;
        _animator = gameObject.GetComponent<Animator>();
    }

   public void ApplyDamage(int damageValue)
    {
        Debug.Log(this.name+" - i was damaged!");
        _currentHp -= damageValue;
        _animator.SetTrigger("GetDamage");
        if (_currentHp <= 0)
            toDie();

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

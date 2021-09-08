using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : Attackable
{
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private Vector2 _attackForceJump = new Vector2(3, 4);
    private int _currentHp;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    
    public bool IsAlive { get; private set; }



    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<Collider2D>();
        _currentHp = _maxHp;
        _animator = gameObject.GetComponent<Animator>();
        
        IsAlive = true;

       
    }

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        Debug.Log(this.name + " - i was damaged!");
        GetDamage(playerPosition);
        Debug.Log(this.name + " - i was damaged!");
        _currentHp -= damageValue;
        _animator.SetTrigger("GetDamage");
        if (_currentHp <= 0)
            toDie();

        AudioManager.Instance.Play("PlayerDamage");
    }

    public virtual void toDie()
    {
        AudioManager.Instance.PlayRandomSound("Death");
        IsAlive = false;
        Debug.Log("its time to die...");
        _collider.enabled = false;
        if (_rb != null)
        {
            _rb.gravityScale = 0;
            _rb.velocity = new Vector2(0, 0);
        }
        _animator.SetTrigger("Dead");
        DisableAllChildColliders();
    }

    private void DisableAllChildColliders()
    {
        Collider2D[] col = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childCollider in col)
        {
            childCollider.enabled = false;
        }
    }

    private void GetDamage(Vector3 damageSource)
    {
        Vector3 direction = new Vector3(damageSource.x > transform.position.x ? -_attackForceJump.x : _attackForceJump.x, _attackForceJump.y, 0);
        if (_rb != null)
            _rb?.AddForce(direction, ForceMode2D.Impulse);
    }

    public override int GetHp()
    {
        return _currentHp;
    }
}

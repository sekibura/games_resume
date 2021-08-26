using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    private float attackPointDefaultPositionX;
    public float attackRange = 0.5f;
    public LayerMask[] enemyLayers;
    private SpriteRenderer spriteRenderer;
    public int damagePoints;

    public float attackRate = 10f;
    private float nextTimeAttack = 0f;

    private PlayerStates _playerStates;

  
    void Start()
    {
        _playerStates = gameObject.GetComponent<PlayerStates>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackPointDefaultPositionX = attackPoint.localPosition.x;
    }

    void Update()
    {
        bool fire = false;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        fire = Input.GetButtonDown("Fire1");
#endif
        fire = fire || SimpleInput.GetButtonDown("Attack");
        

        if (Time.time>=nextTimeAttack && fire && Time.timeScale!=0 && _playerStates.IsControlEnable)
        {
            nextTimeAttack = Time.time + 1f/attackRate;
            Attack();
        }
    }

    private void Attack()
    {
        attackPoint.localPosition= new Vector3(spriteRenderer.flipX ? -attackPointDefaultPositionX: attackPointDefaultPositionX, attackPoint.localPosition.y, attackPoint.localPosition.z);
        animator.SetTrigger("Attack");
        foreach (var item in enemyLayers)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, item);


            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log(enemy.name + " attacked " + enemy.GetComponent<Attackable>());
                enemy.GetComponent<Attackable>()?.ApplyDamage(damagePoints, transform.position);
            }
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}

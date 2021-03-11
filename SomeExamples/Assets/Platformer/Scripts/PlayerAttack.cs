using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    private float attackPointDefaultPositionX;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private SpriteRenderer spriteRenderer;
    public int damagePoints;

  
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        attackPointDefaultPositionX = attackPoint.localPosition.x;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log(spriteRenderer.flipX);
        attackPoint.localPosition= new Vector3(spriteRenderer.flipX ? -attackPointDefaultPositionX: attackPointDefaultPositionX, attackPoint.localPosition.y, attackPoint.localPosition.z);
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name + " attacked");
            enemy.GetComponent<HealthSystem>().Damage(damagePoints);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

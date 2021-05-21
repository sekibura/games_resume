using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Goblin_State_Attack : State
{
    private float _minDistanceToAttack = 0.2f;
 
    public override void Run()
    {
        
        float distance = Vector3.Distance(Character.transform.position, Character.Player.transform.position);
        if ( distance < _minDistanceToAttack)
            AttackPlayer();
        else
            MoveToPlayer();
        if (distance >= 5)
            IsFinished = true;
    }

    private void MoveToPlayer()
    {
        Character.Animator.SetBool("IdleWalking", true);
        Vector3 target = new Vector3(Character.Player.transform.position.x, Character.transform.position.y, Character.transform.position.y);
        Character.MoveTo(target);
    }

    private void AttackPlayer()
    {

        Character.Animator.SetTrigger("Attack");
        ApplyDamageToPlayer();
                
    }

    private void ApplyDamageToPlayer()
    {
        //Character.Player.GetComponent<HealthSystem>().ApplyDamage()
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Goblin_State_Attack : State
{
    private float _minDistanceToAttack = 1f;
    private GameObject _player;
    private float _speed = 3f;
    private float _lastTimeAttack = 0;
    

    [SerializeField]
    private float _attackDistance = 5;

    [SerializeField]
    private float _attackDelay = 0.5f;

    [SerializeField]
    private int _damageValue = 2;

    public override void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Run()
    {
        
        float distance = (Character.transform.position - Character.Player.transform.position).magnitude;
//        Debug.Log("attack " + distance);

        if (distance < _minDistanceToAttack)
            AttackPlayer();
        else
            MoveToPlayer();

        if (distance >= _attackDistance)
            IsFinished = true;
            
    }

    private void MoveToPlayer()
    {
        //Debug.Log("move to player attack");
        //Character.Animator.SetBool("IdleWalking", true);
        Vector3 target = new Vector3(Character.Player.transform.position.x, Character.transform.position.y, Character.transform.position.z);
        Character.MoveTo(target, _speed);
    }

    private void AttackPlayer()
    {
        if (Time.time > _lastTimeAttack + _attackDelay)
        {
            Character.Animator.SetTrigger("Attack");
            ApplyDamageToPlayer();
            _lastTimeAttack = Time.time;
        }
        
                
    }

    private void ApplyDamageToPlayer()
    {
        Character.Player.GetComponent<Attackable>().ApplyDamage(_damageValue, Character.gameObject.transform.position);
        //Character.Player.GetComponent<PlayerController>().Attacked(Character.gameObject);
    }

    //IEnumerable Finish()
    //{
    //    Debug.LogWarning("FINISH");
    //    _waitForFinish = true;
    //    Character.MoveTo(Character.transform.position,1);
    //    yield return new WaitForSeconds(3);
    //    IsFinished = true;
        
    //}
    
}

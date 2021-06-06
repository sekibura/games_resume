using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mushroom_state_idle : State
{


    [SerializeField]
    private float _attackDistance = 5;




    public override void Init()
    {
        IsFinished = false;
        Character.Animator.SetBool("IdleWalking", false);
        Character.Animator.SetBool("IdleStay", true);

    }
    public override void Run()
    {
        if (IsPlayerNearby())
            Finish();

    }



    private bool IsPlayerNearby()
    {
        return Vector3.Distance(Character.transform.position, Character.Player.transform.position) < _attackDistance;
    }

    private void Finish()
    {
        Debug.Log("finished idle");
        Character.Animator.SetBool("IdleStay", false);
        IsFinished = true;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Goblin_State_Idle : State
{
    private Vector3 _startPosition;
    public float DistancePatrol;
    private Vector3 target;
    private float _currentShift;
    
    


    public override void Init()
    {
        _currentShift = DistancePatrol;
        _startPosition = Character.transform.position;
        target = new Vector3(_startPosition.x + _currentShift, _startPosition.y, _startPosition.z);
        IsFinished = false;
        
        
    }
    public override void Run()
    {
        MoveToPoint();
    }

    private void MoveToPoint()
    {
        Character.Animator.SetBool("IdleWalking", true);
        Character.MoveTo(target); 
        if (Vector3.Distance(Character.transform.position, target) < 1f)
        {
            _currentShift *= -1;
            target = new Vector3(_startPosition.x + _currentShift, _startPosition.y, _startPosition.z);
            
        }
        if (IsPlayerNearby())
            Finish();
    }

    private bool IsPlayerNearby()
    {
        return Vector3.Distance(Character.transform.position, Character.Player.transform.position) < 5;
    }

    private void Finish()
    {
        Debug.Log("finished idle");
        IsFinished = true;
        Character.Animator.SetBool("IdleWalking", false);
    }
}

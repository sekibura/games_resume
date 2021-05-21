using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField]
    private State _idleState;
    [SerializeField]
    private State _attackState;

    private float _speed = 1f;

    private State _currentState;
    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public GameObject Player;
    private SpriteRenderer _spriteRenderer;
    public int Damage = 1;
    private void Start()
    {
        SetState(_idleState);
        Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_currentState.IsFinished)
        {
            _currentState.Run();
        }
        else
        {
            if (IsPlayerNearby())
            {
                SetState(_attackState);
            }
            else
            {
                SetState(_idleState);
            }
        }
     
        
    }

    private void SetState(State state)
    {
        _currentState = Instantiate(state);
        _currentState.Character = this;
        _currentState.Init();
    }

    private bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, Player.transform.position)<5;
    }

    public void MoveTo(Vector3 target)
    {
        float step = _speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (target.x > transform.position.x)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
    }
}

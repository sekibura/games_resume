using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    [SerializeField]
    private State _idleState;
    [SerializeField]
    private State _attackState;
    [SerializeField]
    private float _attackDistance = 5;
    //private float _speed = 2f;

    private State _currentState;
    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public GameObject Player;
    private SpriteRenderer _spriteRenderer;
    public int Damage = 1;
    private EnemyHealthSystem _enemyHealthSystem;

    private void Start()
    {
        
        Animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyHealthSystem = GetComponent<EnemyHealthSystem>();
        SetState(_idleState);
    }

    private void Update()
    {
       // Debug.Log(transform.position - Player.transform.position);
        if (!_currentState.IsFinished && _enemyHealthSystem.IsAlive)
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
        
        return Vector3.Distance(transform.position, Player.transform.position)<_attackDistance;
    }

    public void MoveTo(Vector3 target, float speed)
    {
        //Animator.SetBool("IdleWalking", true);
        if (IsWayClear(target))
        {
            Animator.SetBool("IdleWalking", true);
            Animator.SetBool("IdleStay", false);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (target.x > transform.position.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }
        else
        {
           // Debug.Log("way not clear");
            //Animator.SetTrigger("IdleStay");
            Animator.SetBool("IdleStay", true);
            Animator.SetBool("IdleWalking", false);
        }
        
    }

    private bool IsWayClear(Vector3 target)
    {
        
        RaycastHit2D forwardRay = Physics2D.Raycast(transform.position, target.x < transform.position.x ? Vector2.left : -Vector2.left, 0.5f);
       // Debug.DrawRay(transform.position, target.x < transform.position.x ? Vector2.left : -Vector2.left);

        Vector3 dir = new Vector3(target.x < transform.position.x ? -1 : 1, -0.7f, 0);
        RaycastHit2D groundForward = Physics2D.Raycast(transform.position, dir, 1f);
       // Debug.DrawRay(transform.position, dir);


        if (groundForward.collider != null && forwardRay.collider == null && target!=transform.position)
            return groundForward.collider.CompareTag("Ground");
        else
            return false;

    }   


}

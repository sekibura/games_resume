using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicObject
{
    private float _jumpTakeOffSpeed = 10;
    private float _jumpAttacked = 8;
    private float _maxSpeed = 7;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector2 _move = Vector2.zero;

    //-----------attack impact
    private float _shift = 2f;
    private float _currentShift;
    private float _shiftDuration = 2.5f;
    private float _shiftStopTime;
    bool _isShifting = false;
    private float _t = 0.0f;
    private float _deltaAttackImpactX = 0.2f;

    // jump time delay
    private float _timeAfterGroundedToJump = 0.2f;
    private float _timeLastGrounded;

    public Transform PointA;
    public Transform PointB;
    public LayerMask groundLayers;

    
    private PlayerStates _playerStates;

    private void Awake()
    {
        _playerStates = gameObject.GetComponent<PlayerStates>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        if (!_playerStates.IsControlEnable)
            return;

        SaveTimeAtGround();
        _move = Vector2.zero;
        float _shiftValue = 0f;

        float horizontal = 0f;
        bool jump = false;
        if (!_isShifting)
        {

#if UNITY_ANDROID
            horizontal = SimpleInput.GetAxis("Horizontal");
            jump = SimpleInput.GetButtonDown("Jump");
#else
            horizontal = Input.GetAxis("Horizontal");
            jump = Input.GetButtonDown("Jump");
#endif
            //_move.x = Input.GetAxis("Horizontal");
            _move.x = horizontal;
        }       
        //apply attack impulse
        else
        {            
            _shiftValue = Mathf.Lerp(_currentShift, 0, _t);
            _t += _shiftDuration * Time.deltaTime;

            //_move.x = _shiftValue+ Input.GetAxis("Horizontal");
            //_move.x = _shiftValue + Input.GetAxis("Horizontal");
            _move.x = _shiftValue + horizontal;
            if (Mathf.Abs(_shiftValue) < 0.01)
            {
                _isShifting = false;
                _shiftValue = 0f;
                _t = 0f;
            }                
        }
                        

        if (jump && IsPossibleToJump())
        {
            velocity.y = _jumpTakeOffSpeed;

            AudioManager.Instance.Play("PlayerJump");

            //fix double jump
            _timeLastGrounded = _timeAfterGroundedToJump;
        }
        else if(jump)
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        bool flipSprite = (_spriteRenderer.flipX ? (horizontal > 0.01f) : (horizontal < -0.01f));
        if (flipSprite)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        _animator.SetBool("grounded", grounded);
        _animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / _maxSpeed);
        _animator.SetFloat("velocityY", velocity.y);
        

        targetVelocity = _move * _maxSpeed;
    }

    public void Attacked(Vector3 enemy)
    {
        
        velocity.y =  _jumpAttacked;
        _shiftStopTime = Time.time + _shiftDuration;
        _currentShift= enemy.x > gameObject.transform.position.x ? -_shift : _shift;

        float _playerX = gameObject.transform.position.x;
        float _enemyX = enemy.x;

        //from left side
        if (_enemyX-_deltaAttackImpactX > _playerX)
            _currentShift = -_shift;
        //from rightside
        else if (_enemyX+_deltaAttackImpactX < _playerX)
            _currentShift = _shift;
        //from upside
        else if (_playerX > _enemyX - _deltaAttackImpactX && _playerX < _enemyX + _deltaAttackImpactX) 
        {
            _isShifting = false;
            return;
        }

        _isShifting = true;
        
    }

    private void SaveTimeAtGround()
    {
        if (grounded)
            _timeLastGrounded = Time.time;
    }

    private bool IsPossibleToJump()
    {
        return grounded || Time.time < (_timeLastGrounded + _timeAfterGroundedToJump) || IsGroundNearby();
    }

    private bool IsGroundNearby()
    {
        Collider2D ground = Physics2D.OverlapArea(PointA.transform.position, PointB.transform.position, groundLayers);
        if (ground != null)
            return true;
        else
            return false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("grounded");
        }
    }
}

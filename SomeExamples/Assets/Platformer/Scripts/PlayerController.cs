using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicObject
{
    public float jumpTakeOffSpeed = 7;
    public float maxSpeed = 7;


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 move = Vector2.zero;

    //-----------attack impact
    private float shift = 0f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
        

        targetVelocity = move * maxSpeed;
    }

    public void Attacked()
    {
        
        velocity.y = velocity.y>0? -jumpTakeOffSpeed:jumpTakeOffSpeed;
        targetVelocity.x = velocity.x > 0 ? -maxSpeed : maxSpeed;
        
        
    }
}

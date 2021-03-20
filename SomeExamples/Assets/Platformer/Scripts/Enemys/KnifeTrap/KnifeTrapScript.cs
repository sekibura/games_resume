using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTrapScript : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetBool("InZone", true);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetBool("InZone", false);

        }
    }

   

}

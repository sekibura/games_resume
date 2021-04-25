using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private Animator _animator;
    private bool isAlive = true;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Alive()
    {
        if (!isAlive)
        {
            _animator.SetTrigger("Alive");
            isAlive = true;
        }
        
    }

    public void Broke()
    {
        if (isAlive)
        {
            _animator.SetTrigger("Broke");
            isAlive = false;
        }
    }




}

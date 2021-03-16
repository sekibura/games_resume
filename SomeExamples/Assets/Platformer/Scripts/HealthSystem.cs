using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHp;
    private int currentHp;
    private Animator animator;
 

    void Start()
    {
        currentHp = maxHp;
        animator = gameObject.GetComponent<Animator>();
    }

   public void Damage(int damageValue)
    {
        Debug.Log(this.name+" - i was damaged!");
        currentHp -= damageValue;
        animator.SetTrigger("GetDamage");
        if (currentHp <= 0)
            toDie();

    }

    private void toDie()
    {
        Debug.Log("its time to die...");
        animator.SetTrigger("Dead");
        
        
    }

    public void AddHp(int value)
    {
        currentHp += value;
        if (currentHp > maxHp)
            currentHp = maxHp;
    }
}

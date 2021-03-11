using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHp;
    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

   public void Damage(int damageValue)
    {
        Debug.Log(this.name+" - i was damaged!");
        currentHp -= damageValue;
        //TODO
        //play hurt anim!
        if (currentHp <= 0)
            toDie();

    }

    private void toDie()
    {
        Debug.Log("its time to die...");
        // TODO
        //play die anim
    }

    public void AddHp(int value)
    {
        currentHp += value;
        if (currentHp > maxHp)
            currentHp = maxHp;
    }
}

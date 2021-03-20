using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack : MonoBehaviour
{
    private int _damageValue = 30;

 
    
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().Attacked(gameObject); // impulse player
            collision.gameObject.GetComponent<HealthSystem>().ApplyDamage(_damageValue); // 
        }
    }

 
}

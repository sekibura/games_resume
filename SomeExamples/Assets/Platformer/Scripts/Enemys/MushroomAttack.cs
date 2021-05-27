using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack : MonoBehaviour
{
    [SerializeField]
    private int _damageValue = 2;

 
    
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThisPlayer(collision))
        {
            //collision.GetComponent<PlayerController>().Attacked(gameObject); // impulse player
            collision.gameObject.GetComponent<HealthSystem>().ApplyDamage(_damageValue,gameObject.transform.position); // 
        }
    }

    private bool IsThisPlayer(Collider2D collider)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTrapBladesScript : MonoBehaviour
{
    [SerializeField]
    private int _damageValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("attack knides");
        //collision.gameObject.GetComponent<PlayerController>().Attacked(gameObject); // impulse player
        collision.gameObject.GetComponent<HealthSystem>().ApplyDamage(_damageValue,gameObject.transform.position);
    }

}

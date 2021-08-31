using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attackable attackable = collision.gameObject.GetComponent<Attackable>();
        if (attackable != null)
            attackable?.ApplyDamage(10000, gameObject.transform.position);
        else
            Destroy(collision.gameObject);

        
    }
}

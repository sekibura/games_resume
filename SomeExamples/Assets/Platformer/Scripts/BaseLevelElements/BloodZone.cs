using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DeadBody")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

    }
}

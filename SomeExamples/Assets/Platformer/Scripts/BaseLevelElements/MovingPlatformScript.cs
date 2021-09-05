using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " enter to " + gameObject.name);
        collision.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " exit from " + gameObject.name);
        collision.transform.parent = null;
    }

    //private Rigidbody2D rbody;

    //private bool isOnPlatform;

    //private Rigidbody2D platformRBody;

    //private void Awake()
    //{
    //    rbody = GetComponent<Rigidbody2D>();
    //}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Platform")
    //    {
    //        platformRBody = col.gameObject.GetComponent<Rigidbody2D>();
    //        isOnPlatform = true;
    //    }
    //}

    //void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Platform")
    //    {
    //        isOnPlatform = false;
    //        platformRBody = null;
    //    }
    //}

    //void FixedUpdate()
    //{
    //    if (isOnPlatform)
    //    {
    //        Debug.Log("isOnPlatform " + rbody.velocity+ "||Platform: " + platformRBody.velocity);

    //        rbody.velocity = rbody.velocity + platformRBody.velocity;
    //    }
    //}
}

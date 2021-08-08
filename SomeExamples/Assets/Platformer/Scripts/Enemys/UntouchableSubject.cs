using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntouchableSubject : MonoBehaviour
{
    [SerializeField]
    private int _damageValue = 2;

    private float _delayBetweenDamage = 0.1f;
    private float _lastTime = 0;
 
    
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyDamage(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.LogError("trigger stay");
        ApplyDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ApplyDamage(collision.collider);
        //Debug.LogError("collider stay");
    }

    private void ApplyDamage(Collider2D collision)
    {
        if (Time.time > _lastTime + _delayBetweenDamage)
        {
            collision.gameObject.GetComponent<Attackable>()?.ApplyDamage(_damageValue, gameObject.transform.position);
            _lastTime = Time.time;
        }
    }

    IEnumerator DamageAfterSeconds(Collider2D collision, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        collision.gameObject.GetComponent<Attackable>()?.ApplyDamage(_damageValue, gameObject.transform.position);
    }

}

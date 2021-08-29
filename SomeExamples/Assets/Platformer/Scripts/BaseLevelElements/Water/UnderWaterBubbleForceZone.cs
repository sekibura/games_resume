using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterBubbleForceZone : MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
    }
  
    public void SwitchColliderEnable()
    {
        _collider.enabled = _collider.enabled ? false : true; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " enter in:" + gameObject.name);
    }
}

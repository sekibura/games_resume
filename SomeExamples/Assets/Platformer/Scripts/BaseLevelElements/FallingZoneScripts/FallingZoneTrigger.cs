using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingZoneTrigger : MonoBehaviour
{
    private GameObject _parent;
    FallingZoneParent _fallingZone;
    [SerializeField]
    private bool isStart;
    private void Start()
    {
        _parent = gameObject.transform.parent.gameObject;
        _fallingZone = _parent.GetComponent<FallingZoneParent>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
        if (collision.transform.tag == "Player")
        {
            if (isStart)
                _fallingZone.OnStartFalling();
            else
                _fallingZone.OnFinishFalling();
        }
    }
}

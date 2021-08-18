using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _events;
    [SerializeField]
    private bool _doOnce = true;
    [SerializeField]
    private bool _done = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_events != null && !_done)
        {
            _events?.Invoke();
        }
        _done = true;
    }
}

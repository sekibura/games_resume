using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackedTrigger : Attackable
{
    [SerializeField]
    private UnityEvent _events;
    [SerializeField]
    private bool _doOnce = true;
    [SerializeField]
    private bool _done = false;

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        if (_events != null && !_done)
        {
            _events?.Invoke();
        }
        _done = true;
    }
}

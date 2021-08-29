using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAttackAction : Attackable
{
    [SerializeField]
    private UnityEvent _event;

    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        if (_event != null)
        {
            Debug.LogError("ondestroy");
            _event.Invoke();
        }
    }

    public override int GetHp()
    {
        return 0;
    }
}

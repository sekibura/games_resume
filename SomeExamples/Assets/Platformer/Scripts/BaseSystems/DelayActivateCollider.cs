using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActivateCollider : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider2D;
    [SerializeField]
    private float _delay;
    private void Start()
    {
        _collider2D.enabled = false;
        StartCoroutine(DelayedActivate());
    }

    private IEnumerator DelayedActivate()
    {
        yield return new WaitForSeconds(_delay);
        _collider2D.enabled = true;
    }

}

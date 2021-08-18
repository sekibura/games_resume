using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyAction : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _event;
    private void OnDestroy()
    {
        if (_event != null)
        {
            Debug.LogError("ondestroy");
            _event.Invoke();
        }
    }
}

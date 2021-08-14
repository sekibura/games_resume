using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.Localization;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent<string> events;
    public bool DoOnce = true;
    private bool _done = false;
    
    //public LocalizedString localizedString;
    string _message="";

    private void Start()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var localizedText = localizedString.GetLocalizedString();
        //if (localizedText.IsDone)
        //{
        //    Debug.LogError(localizedText.Result);
        //}

        Debug.LogError(_message);
        if (!_done && events != null)
        {
            events?.Invoke(_message);
            _done = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Unit
{
    private bool _isFirstMeet = true;

    private void Start()
    {
        InitBehaviours();
    }
    protected override void InitBehaviours()
    {
        _speakable = new SimplePhrase(new List<string>{"Hello, how are you?" });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ToSpeak(this.gameObject);
            if (_isFirstMeet)
            {
                _isFirstMeet = false;
                _speakable = new SimplePhrase();
            }
        }
    }
}

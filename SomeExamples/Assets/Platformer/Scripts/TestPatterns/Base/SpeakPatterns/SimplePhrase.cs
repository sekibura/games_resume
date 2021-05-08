using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePhrase : ISpeakable
{
    
    private List<string> _speech = new List<string>{ "How are you?", "Be careful, friend.", "I just want to be happy...", "Oh, is it you again?" };
    private float _deltaTime = 2;
    private float _lastTime;

    public SimplePhrase() 
    { 

    }
    public SimplePhrase(List<string> speechs)
    {
        _speech = speechs;
    }

    public void ToSpeak(Vector3 position)
    {
        if (Time.time > _lastTime + _deltaTime)
        {
            string message = "";
            if (_speech.Count != 0)
            {
                System.Random random = new System.Random();
                int number = random.Next(0, _speech.Count);
                message = _speech[number];
                _speech.RemoveAt(number);
            }
            else
                message = "...";
            _lastTime = Time.time;
            SpawnTextSystem.Instance.CreateText(message, position);
        }
       
        

    }
}

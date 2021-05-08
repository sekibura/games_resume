using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit :MonoBehaviour
{
    protected ISpeakable _speakable;

    protected abstract void InitBehaviours();

    public void ToSpeak(GameObject obj)
    {
        _speakable.ToSpeak(obj.transform.position);
    }

    public void ChangeSpeakBehaviour(ISpeakable pattern)
    {
        _speakable = pattern;
    }
}

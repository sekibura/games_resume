using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameSettings : MonoBehaviour
{

    public static GlobalGameSettings Instance;
    public Language TextLanguage { get; set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = 120;

        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}

public enum Language
{
    Russian,
    English
}

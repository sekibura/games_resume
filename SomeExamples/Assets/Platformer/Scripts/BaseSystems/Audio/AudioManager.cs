using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        Instance.Play("BackGroundMusic");
    }

    public void Play(string name)
    {
        if (Time.timeScale != 0)
        {
            Sound s = Array.Find(sounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found!");
                return;
            }
            s.Source.Play();
        }
    }
}

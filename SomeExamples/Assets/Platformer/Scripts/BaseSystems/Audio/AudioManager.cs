using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    [SerializeField]
    private RandomSound[] _randomSounds; 

    public static AudioManager Instance;

    [Range(0.0f, 1.0f)]
    public float volume = 1f;

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

            s.Source.volume = s.Volume*volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
        foreach (RandomSound s in _randomSounds)
        {
            foreach (Sound sound in s.Sounds)
            {

                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;

                sound.Source.volume = sound.Volume * volume * s.volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;
            }
        
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

    public void PlayRandomSound(string name)
    {
        if (Time.timeScale != 0)
        {
            RandomSound s = Array.Find(_randomSounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found!");
                return;
            }
            UnityEngine.Random.seed = System.DateTime.Now.Millisecond;
            int number = UnityEngine.Random.Range(0, s.Sounds.Length);
            s.Sounds[number].Source.Play();
        }
    }

    public void StopSound()
    {
        if (Time.timeScale != 0)
        {
            Sound s = Array.Find(sounds, sound => sound.Name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not found!");
                return;
            }
            s.Source.Stop();
        }
    }
}

[Serializable]
public class RandomSound
{
    public string Name;
    public Sound[] Sounds;
    [Range(0.0f, 1.0f)]
    public float volume = 1f;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Unit
{
    private Animator _animator;
    [SerializeField]
    List<string> _speech;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        InitBehaviours();
    }
    protected override void InitBehaviours()
    {
        _speakable = new SimplePhrase(_speech);
        //_speakable = new SimplePhrase(new List<string> { "*tweet-tweet*", "You are lost?", "I do not advise you to go further ...", "bird song" });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _animator.SetBool("IsPlayerNearby", true);
            _speakable.ToSpeak(transform.position);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            _animator.SetBool("IsPlayerNearby", false);
    }
}

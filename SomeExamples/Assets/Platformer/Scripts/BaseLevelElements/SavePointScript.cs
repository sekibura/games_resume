using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SavePointScript : MonoBehaviour
{

    private GameStateScript _gameStateScript;
    private bool _isSaved;
    private Light2D _light2D;
    private Animator _animator;
    

    private void Start()
    {
        _gameStateScript = GameObject.FindObjectOfType<GameStateScript>();
        _light2D = gameObject.GetComponentInChildren<Light2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Saved");
            if (!_isSaved)
            {
                _isSaved = true;
                _gameStateScript.SaveGame(gameObject);
                _light2D.enabled = false;
                _animator.SetTrigger("Save");
            }
        }
    }
}

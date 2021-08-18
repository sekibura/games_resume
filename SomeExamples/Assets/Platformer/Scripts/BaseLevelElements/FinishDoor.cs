using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDoor : MonoBehaviour
{
    [SerializeField]
    private bool _isFinishDoor = false;
    [SerializeField]
    private GameObject _exitDoor;
    private GameStateScript _gameStateScript;
    [SerializeField]
    private bool _isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThisPlayer(collision) && _isActive)
        {
            NextLevel(collision.gameObject);
        }
    }

    private void NextLevel(GameObject player)
    {
        //TODO
        //to next level!
        Debug.Log("Switch lvl");

        //if last door on level
        if (_isFinishDoor)
            SwitchScene();
        else
            MoveToExitDoor(player);
    }

    private void SwitchScene()
    {
        _gameStateScript = GameObject.Find("UI")?.GetComponent<GameStateScript>();
        _gameStateScript?.LvlCompleted();
        //if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount-1)
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //else
        //    SceneManager.LoadScene(0);
    }

    private void MoveToExitDoor(GameObject player)
    {
        if(_exitDoor!=null)
        {
            player.transform.position = _exitDoor.transform.position;
            PlaySoundTeleport();
        }

    }

    private bool IsThisPlayer(Collider2D collider)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }

    private void PlaySoundTeleport()
    {
        AudioManager.Instance.Play("Door");
    }

    public void SetEnable(bool value)
    {
        Debug.LogError(gameObject.name + "setEnable" + value);
        _isActive = value;
    }
}

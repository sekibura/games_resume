using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDoor : MonoBehaviour
{
    public bool IsFinishDoor = false;
    public GameObject ExitDoor;
    private GameStateScript _gameStateScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThisPlayer(collision))
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
        if (IsFinishDoor)
            SwitchScene();
        else
            MoveToExitDoor(player);
    }

    private void SwitchScene()
    {
        _gameStateScript = GameObject.Find("UI")?.GetComponent<GameStateScript>();
        _gameStateScript?.LvlCompleted();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void MoveToExitDoor(GameObject player)
    {
        if(ExitDoor!=null)
        {
            player.transform.position = ExitDoor.transform.position;
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
}

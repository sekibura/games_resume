using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoorTutorial: MonoBehaviour
{
    [SerializeField]
    private bool _isFinishDoor = false;
    [SerializeField]
    private GameObject _exitDoor;
    private GameStateScript _gameStateScript;
    [SerializeField]
    private bool _isActive = true;
    [SerializeField]
    private string[] _targetTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool missionDone = true;

        foreach(string tag in _targetTags)
        {
            var target = GameObject.FindGameObjectsWithTag(tag);
            missionDone = missionDone && !(target.Length > 0);
        }

        if (IsThisPlayer(collision) && _isActive &&missionDone)
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void MoveToExitDoor(GameObject player)
    {
        if (_exitDoor != null)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour
{
    private GameObject _pickUpAble;
    private List<GameObject> _childs;
    private bool _isPaused = false;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _deathMenu;
    private void Start()
    {
        _pickUpAble = GameObject.FindGameObjectWithTag("PickUpAble");
        //_pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if (_pickUpAble != null)
        {
            _childs = AllChilds(_pickUpAble);
            Debug.Log(_childs.Count);
        }
    }

    public void Pause()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            SetActiveObject(_pauseMenu, true);
            Time.timeScale = 0f;
            
        }
        else
        {
            _isPaused = false;
            SetActiveObject(_pauseMenu,false);
            Time.timeScale = 1f;
        }
        Debug.Log("pause");
    }

    public void GameOver()
    {
        SetActiveObject(_deathMenu, true);
    }

    public void Restart()
    {
        Debug.Log("restart");
        StartCoroutine(HideDeathScreen());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }


    private void SetAllActive(bool value)
    {
        foreach(GameObject child in _childs)
        {
            child.SetActive(value);
        }
    }


    //private void OnGUI()
    //{
    //    Event e = Event.current;

    //    if (e.isKey)
    //    {
    //        switch (e.keyCode)
    //        {
    //            case KeyCode.Escape:
    //                SetPause();
    //                break;
    //            case KeyCode.R:
    //                Restart();
    //                break;
    //        }
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    private void SetActiveObject(GameObject gameObject, bool value)
    {
        if (gameObject != null)
            gameObject.SetActive(value);
        else
            Debug.LogWarning(gameObject + " is null!");
    }

    IEnumerator HideDeathScreen()
    {
        yield return new WaitForSeconds(2);
        SetActiveObject(_deathMenu, false);
    }

}

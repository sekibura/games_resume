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
    [SerializeField]
    private GameObject _lvlComletedMenu;
    private Animator _screenToDark;
    private GameObject _lastSavePoint;

    public bool IsPlayerControlActive { get; set; } = true;

    private PlayerStates _playerStates;

    private void Start()
    {
        _playerStates = GameObject.Find("Player")?.GetComponent<PlayerStates>();
        _pickUpAble = GameObject.FindGameObjectWithTag("PickUpAble");
        _screenToDark = GameObject.Find("ScreenToDark")?.GetComponent<Animator>();
        //_pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if (_pickUpAble != null)
        {
            _childs = AllChilds(_pickUpAble);
            //Debug.Log(_childs.Count);
        }
    }

    public void PauseMenu()
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

    public void PauseGame()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0f;

        }
        else
        {
            _isPaused = false;
            Time.timeScale = 1f;
        }
        Debug.Log("pause");
    }

    public void PausePlayer(bool value)
    {
        _playerStates.IsControlEnable = !value;
    }


    public void GameOver(bool value)
    {
        SetActiveObject(_deathMenu, value);
    }

    public void Restart()
    {
        Debug.Log("restart");
        Time.timeScale = 1;
        //StartCoroutine(HideDeathScreen());
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


    private void Update()
    {
        bool esc;
#if UNITY_ANDROID
        esc = SimpleInput.GetButtonDown("Pause");
#else
        esc = Input.GetKeyDown(KeyCode.Escape);
#endif
        if (esc)
        {
            PauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCheckPoint();
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

    public void LvlCompleted()
    {
        StartCoroutine(ShowLvlCompMenu());
    }
    private IEnumerator ShowLvlCompMenu()
    {
        
        _screenToDark?.Play("ToDark");
        yield return new WaitForSeconds(1);
        GameObject player = GameObject.Find("Player");
        player?.SetActive(false);
        _lvlComletedMenu?.SetActive(true);
        _screenToDark?.Play("ToLight");
        

        
    }

    public void SaveGame(GameObject savePoint)
    {
        _lastSavePoint = savePoint;
    }

    public void LoadCheckPoint()
    {
        GameObject _player = GameObject.Find("Player");
        

        if (_lastSavePoint != null)
        {
            _player.transform.position = _lastSavePoint.transform.position;
            _player?.GetComponent<HealthSystem>()?.Alive();
        }            
        else
            Restart();

    }
   
}

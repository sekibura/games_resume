using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameObject _darkPanel;
    private void Start()
    {
        AudioManager.Instance.Play("Music");
        _darkPanel = GameObject.Find("ScreenToDark");
        _darkPanel?.GetComponent<Animator>().Play("ToLight");
    }
    public void StartGame()
    {
        _darkPanel?.GetComponent<Animator>().Play("ToDark");
        StartCoroutine(LoadGamne());
        
    }

    IEnumerator LoadGamne()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("PlatformerLvl");
    }

    public void ExitGame()
    {
        _darkPanel?.GetComponent<Animator>().Play("ToDark");
        Application.Quit();
    }
}

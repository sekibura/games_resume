using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.Play("Music");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("PlatformerLvl");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private Animator _screenToDarkAnimator;
    private void Start()
    {
        _screenToDarkAnimator = GameObject.Find("ScreenToDark").GetComponent<Animator>();
        _screenToDarkAnimator?.Play("ToLight");
    }
    public void ExitGame()
    {
        _screenToDarkAnimator?.Play("ToDark");
        Time.timeScale = 1;
        StartCoroutine(Exit());
    }

    public void GoToMenu()
    {
        _screenToDarkAnimator?.Play("ToDark");
        Time.timeScale = 1;
        StartCoroutine(LoadMenu());
    }

    public void NextLevel()
    {
        Debug.LogError(SceneManager.GetActiveScene().buildIndex + " " + SceneManager.sceneCountInBuildSettings);
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
        
    }
    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }


}

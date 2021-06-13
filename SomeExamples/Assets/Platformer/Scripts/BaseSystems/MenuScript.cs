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

#if UNITY_ANDROID
        GameObject.Find("AndroidButtons")?.SetActive(true);
#else
        GameObject.Find("AndroidButtons")?.SetActive(false);
#endif
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

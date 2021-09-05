using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class PopUpMessageWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textField;

    [SerializeField]
    private int _numberLettersInField = 165;

    [SerializeField]
    private float _delayLetters= 0.1f;
    [SerializeField]
    private float _delayParts = 3f;
    [SerializeField]
    private float _delayWords = 1f;

    [SerializeField]
    private float _openAnimationDuration = 0.5f;
    [SerializeField]
    private float _closeAnimationDuration = 0.5f;

    [SerializeField]
    private GameObject _exitButton;

    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField]
    private GameStateScript _gameStateScript;

   

    public void ShowMessage(string message)
    {   
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.pitch = 2;
        _audioSource.volume = 0.2f;
        _exitButton.SetActive(false);
        _animator.Play("OpenPopUpMessageWindow");
        _textField.text = "";
        StartShowMessage(message);
    }

    private void StartShowMessage(string message)
    {
        //Debug.LogError("showmessage");
        List<string> parts = FitTextToParts(message);
        //Debug.LogError("after fit");
        _gameStateScript.PausePlayer(true);
        StartCoroutine(ShowPartOfMessage(parts));
    }

    private List<string> FitTextToParts(string text)
    {
        //Debug.LogError("FIT");
        List<string> parts = new List<string>();
        if(text.Length > _numberLettersInField)
        {
            StringBuilder part = new StringBuilder();
            string[] words = text.Split(' ');
            foreach(string word in words)
            {
                if(part.Length + word.Length > _numberLettersInField)
                { 
                    part.Append("...");
                    parts.Add(part.ToString());
                    Debug.LogError(part.ToString());
                    part.Clear();
                    part.Append("...");
                }
                part.Append(" " + word);
            }
            parts.Add(part.ToString());
            Debug.LogError(part.ToString());
        }
        else
        {
            parts.Add(text);
        }
        return parts;
    }

    private IEnumerator ShowPartOfMessage(List<string> parts)
    {
        
        foreach (string part in parts)
        {
            _textField.maxVisibleCharacters = 0;
            _textField.text = part;
            //Debug.LogError(part);
           
            int length = _textField.text.Length;
            for (int i = 0; i < length; i++)
            {
                _textField.maxVisibleCharacters = i;
            
                if (_textField.text[i] == '.' || _textField.text[i] == '!' || _textField.text[i] == '?')
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                }
                else if (_textField.text[i] == ' ')
                    yield return new WaitForSecondsRealtime(0.1f);
                else
                {
                    _audioSource.Play();
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                

            }
            _textField.maxVisibleCharacters = _textField.text.Length;
            yield return new WaitForSeconds(_delayParts);
        }
        _exitButton.SetActive(true);
        _animator.Play("ShowButton");
    }

    public void ExitBtn()
    {
        StartCoroutine(CloseWindow());
    }
    private IEnumerator CloseWindow()
    {
        _animator.Play("ClosePopUpMessageWindow");
        yield return new WaitForSecondsRealtime(1);
        _textField.text = "";
        gameObject.SetActive(false);
        _gameStateScript.PausePlayer(false);
    }
}

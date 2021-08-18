using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    private int _score = 0;
    private TMP_Text _scoreTextField;

    private void Start()
    {
        GameObject textObj = GameObject.FindGameObjectWithTag("CoinsScore");
        _scoreTextField = textObj?.GetComponent<TMP_Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            AddPoint();
            FindObjectOfType<AudioManager>().Play("Coin");
        }
    }
    private void AddPoint()
    {
        if(_scoreTextField != null)
        {
            _score++;
            SetScore(_score);
        }
    }

    public int GetScore()
    {
        return _score;
    }

    private void SetScore(int value)
    {
        if (value >= 0 && _scoreTextField != null)
            _scoreTextField.text = value.ToString();
    }
}


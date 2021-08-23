using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesBar : MonoBehaviour
{
    private int _currentAirValue;
    [SerializeField]
    private GameObject _bubblePrefab;
    private List<GameObject> bubbles = new List<GameObject>();
    private int _max;

    public void SetBubblesValue(int value)
    {
        if (value < _currentAirValue)
        {
            for (int i = _currentAirValue-1; i >= value ; i--)
            {
                bubbles[_max - i - 1].GetComponent<Animator>().Play("BubbleRemove");
            }
            _currentAirValue = value;
        }
    }   

    public void Init(int max)
    {
        foreach (GameObject bub in bubbles)
        {
            Destroy(bub);
        }
        bubbles.Clear();

        for (int i = 0; i < max; i++)
        {
            GameObject bubble = (GameObject)Instantiate(_bubblePrefab);
            bubble.transform.SetParent(gameObject.transform);
            bubbles.Add(bubble);
        }
        _currentAirValue = max;
        _max = max;
    }
    

}

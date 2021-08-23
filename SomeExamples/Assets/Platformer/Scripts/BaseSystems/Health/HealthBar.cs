using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPrefab;

    private int _maxHp = 3;
    private int _hp =0;
    private List<HeartController> _hearts;


    public void Init(int maxHp)
    {
        if (maxHp > 0)
            _maxHp = maxHp;
        else
            _maxHp = 3;

        _hp = _maxHp;

        SpawnHearts(_maxHp);
    }

    private void SpawnHearts(int count)
    {
        _hearts = new List<HeartController>();
        for (int i = 0; i < count; i++)
        {
            GameObject heart = (GameObject)Instantiate(_heartPrefab);
            heart.transform.SetParent(gameObject.transform);

            _hearts.Add(heart.GetComponent<HeartController>());
        }
    }

    public void ApplyDamage(int value)
    {
        if (value > 0)
        {
            if (_hp - value >= 0)
                _hp -= value;
            else
                _hp = 0;
        }
        UpdateBarValue();
    }

    public void SetHealth(int value)
    {
        if (value > 0)
        {
            _hp = value;
        }
        UpdateBarValue();
    }

    private void UpdateBarValue()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < _maxHp-_hp)
                _hearts[i].Broke();
            else 
                _hearts[i].Alive();
        }
    }
}

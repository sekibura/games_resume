using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoitionScript : MonoBehaviour
{
    [SerializeField]
    private int _hpValue;
    private HealthSystem _healthSystem;


    private void Start()
    {
        _healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _healthSystem.AddHp(_hpValue);
            gameObject.SetActive(false);
        }

        
    }
}

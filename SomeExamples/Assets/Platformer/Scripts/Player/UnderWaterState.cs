using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterState : MonoBehaviour
{

    private Attackable _attackable;
    private BubblesBar _bubblesBar;
    private int _maxAirPoints = 6;
    private int _currentAirPoints;


    private void Start()
    {
        _attackable = gameObject.transform.parent.GetComponent<Attackable>();
        _bubblesBar = GameObject.Find("Bubbles")?.GetComponent<BubblesBar>();
        _currentAirPoints = _maxAirPoints;
        if (gameObject.transform.parent.CompareTag("Player"))
            _bubblesBar.Init(_maxAirPoints);
        InvokeRepeating("UnderwaterBreath", 2f, 2f);
        
    }

    private void UnderwaterBreath()
    {
        if (_attackable == null)
            return;

        if (_attackable.GetHp() <= 0)
            Destroy(this);
      
        //update ui bubbles
        if (_currentAirPoints > 0)
        {
            _currentAirPoints -= 1;
            if (gameObject.transform.parent.tag == "Player")
            {
                AudioManager.Instance.PlayRandomSound("Bubbles");
                _bubblesBar.SetBubblesValue(_currentAirPoints);
            }
                

        }
        else
        {
            _attackable.ApplyDamage(1, gameObject.transform.position);
        }
    }

    public void ResetBubbles()
    {
        if (gameObject.transform.parent.tag == "Player")
        {
            _bubblesBar.SetBubblesValue(0);
            Debug.Log("reset buble");
        }
    }
}

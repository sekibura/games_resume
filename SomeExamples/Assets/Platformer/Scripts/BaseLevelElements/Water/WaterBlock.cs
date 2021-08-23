using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystem;
    private float _delaySpawn = 0.1f;
    private float _lastTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > _lastTime + _delaySpawn)
        {
            Instantiate(_particleSystem, collision.gameObject.transform.position, Quaternion.identity);
            _lastTime = Time.time;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectForSpawn;


    [SerializeField]
    private Color _colorZone;

    [SerializeField]
    private Vector3 _areaSize;

    [SerializeField]
    private bool _loop;

    [SerializeField]
    private float _repeatRate;

    //[SerializeField]
    //private float _time;


    [SerializeField]
    private bool _onStart;

    [SerializeField]
    [Tooltip("Спавнить шагами или рандомно")]
    private bool _stepsInX;
    [SerializeField]
    private int _stepsCount;

    [SerializeField]
    [Tooltip("Время между полными проходами")]
    private float _timeBetweenLines = 1f;

    private float _x;
    float stepSize;

    [SerializeField]
    [Tooltip("Придавать ли рандомное вращение объектам")]
    private bool _isAddRotation = false;


    private void Start()
    {
        _x = gameObject.transform.position.x - _areaSize.x / 2;
        if (_onStart)
        {
            StartSpawn();
        }
    }

    public void StartSpawn()
    {
         stepSize = _areaSize.x / _stepsCount;
        if (_loop)
            StartCoroutine(Spawn());
        //InvokeRepeating("Spawn", _time, _repeatRate);
        else
            Spawn();
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Random.seed = System.DateTime.Now.Millisecond;

            if (_stepsInX)
            {
                Debug.Log(stepSize);


                //if ((_x + stepSize) > (gameObject.transform.position.x + _areaSize.x / 2) || (_x - stepSize) < (gameObject.transform.position.x - _areaSize.x / 2))
                //    stepSize = -stepSize;

                if ((stepSize > 0 && (_x + stepSize) > (gameObject.transform.position.x + _areaSize.x / 2)) || (stepSize < 0 && (_x + stepSize) < (gameObject.transform.position.x - _areaSize.x / 2)))
                {
                    stepSize = -stepSize;
                    yield return new WaitForSecondsRealtime(_timeBetweenLines);
                }


                _x += stepSize;
            }
            else
                _x = Random.Range(gameObject.transform.position.x - _areaSize.x / 2, gameObject.transform.position.x + _areaSize.x / 2);

            float Y = Random.Range(gameObject.transform.position.y - _areaSize.y / 2, gameObject.transform.position.y + _areaSize.y / 2);
            Vector3 pos = new Vector3(_x, Y, gameObject.transform.position.z);
            GameObject spawned = Instantiate(_objectForSpawn, pos, Quaternion.identity);

            if (_isAddRotation)
            {
                spawned.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20, 20));
            }

            int angle = 0;
            if (Random.Range(0, 2) == 1)
                angle = 90;


            spawned.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            yield return new WaitForSecondsRealtime(_repeatRate);
        }
    }



   

    void OnDrawGizmosSelected()
    {
        Gizmos.color = _colorZone;
        Gizmos.DrawCube(transform.position, _areaSize);
    }
}

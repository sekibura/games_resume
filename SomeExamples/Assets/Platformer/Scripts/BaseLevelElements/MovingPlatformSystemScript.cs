using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovingPlatformSystemScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _platform;
    [SerializeField]
    private Vector3 _areaSize;
    [SerializeField]
    private Color _colorZone;
    [SerializeField]
    private Color _colorPoint;
    [SerializeField]
    private Color _colorStart;
    [SerializeField]
    private Color _colorFinish;
    [SerializeField]
    private Dimension _moveAxis;
    [SerializeField]
    [Tooltip("Начальная позиция платформы относительно крайних точек")]
    [Range(0.0f, 1.0f)]
    private float _startFromPosition;

    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;


    private GameObject spawnedPlatform;

    private void Start()
    {
        Vector3 position = CalcStartPosition();

        spawnedPlatform = Instantiate(_platform, position, Quaternion.identity);

        startPosition = CalcStartPos();
        endPosition = CalcFinishPosition();
        StartCoroutine(Vector3LerpCoroutine(spawnedPlatform, endPosition, speed));
    }

    private Vector3 CalcStartPos()
    {
        Vector3 position;
        switch (_moveAxis)
        {
            case Dimension.X:
                {
                    float x = transform.position.x - _areaSize.x / 2;
                    position = new Vector3(x, transform.position.y, transform.position.z);
                }
                break;
            case Dimension.Y:
                {
                    float y = transform.position.y - _areaSize.y / 2;
                    position = new Vector3(transform.position.x, y, transform.position.z);
                }
                break;
            case Dimension.Z:
                {
                    float z = transform.position.z - _areaSize.z / 2;
                    position = new Vector3(transform.position.x, transform.position.y, z);
                }
                break;
            default:
                position = gameObject.transform.position;
                break;
        }
        return position;
    }
    private Vector3 CalcFinishPosition()
    {
        Vector3 position;
        switch (_moveAxis)
        {
            case Dimension.X:
                {
                    float x = transform.position.x + _areaSize.x / 2 ;
                    position = new Vector3(x, transform.position.y, transform.position.z);
                }
                break;
            case Dimension.Y:
                {
                    float y = transform.position.y + _areaSize.y / 2 ;
                    position = new Vector3(transform.position.x, y, transform.position.z);
                }
                break;
            case Dimension.Z:
                {
                    float z = transform.position.z + _areaSize.z / 2 ;
                    position = new Vector3(transform.position.x, transform.position.y, z);
                }
                break;
            default:
                position = gameObject.transform.position;
                break;
        }
        return position;
    }
    private Vector3 CalcStartPosition()
    {
        Vector3 position;
        switch (_moveAxis)
        {
            case Dimension.X:
                {
                    float x = transform.position.x - _areaSize.x / 2 + _areaSize.x * _startFromPosition;
                    position = new Vector3(x, transform.position.y, transform.position.z);
                }
                break;
            case Dimension.Y:
                {
                    float y = transform.position.y - _areaSize.y / 2 + _areaSize.y * _startFromPosition;
                    position = new Vector3(transform.position.x, y, transform.position.z);
                }
                break;
            case Dimension.Z:
                {
                    float z = transform.position.z - _areaSize.z / 2 + _areaSize.z * _startFromPosition;
                    position = new Vector3(transform.position.x, transform.position.y, z);
                }
                break;
            default:
                position = gameObject.transform.position;
                break;
        }
        return position;
    }


    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = _colorZone;
    //    Gizmos.DrawCube(transform.position, _areaSize);

    //    Vector3 position = CalcStartPosition();
       
    //    Gizmos.color = _colorPoint;
    //    Gizmos.DrawSphere(position, 0.1f);

    //    Gizmos.color = _colorStart;
    //    Gizmos.DrawSphere(CalcStartPos(), 0.2f);
    //    Gizmos.color = _colorFinish;
    //    Gizmos.DrawSphere(CalcFinishPosition(), 0.2f);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = _colorZone;
        Gizmos.DrawCube(transform.position, _areaSize);

        Vector3 position = CalcStartPosition();

        Gizmos.color = _colorPoint;
        Gizmos.DrawSphere(position, 0.1f);

        Gizmos.color = _colorStart;
        Gizmos.DrawSphere(CalcStartPos(), 0.2f);
        Gizmos.color = _colorFinish;
        Gizmos.DrawSphere(CalcFinishPosition(), 0.2f);
    }

    private enum Dimension
    {
        X,
        Y,
        Z
    }


    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        if (spawnedPlatform.transform.position == endPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(spawnedPlatform, startPosition, speed));
        }
        if (spawnedPlatform.transform.position == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(spawnedPlatform, endPosition, speed));
        }
    }
}

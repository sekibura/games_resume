using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingZoneParent : MonoBehaviour
{
    private GameObject _startFall, _endFall;


    [SerializeField]
    private float _screenY;

    [SerializeField]
    private float _ortSize;

    private float _screenYDefault;
    private float _ortSizeDefault;

    private CinemachineFramingTransposer _cinemachineFramingTransposer;
    private CinemachineVirtualCamera _vcam;

    private void Start()
    {
        Transform start = gameObject.transform.Find("Start");
        Transform finish = gameObject.transform.Find("Finish");
        GameObject _camera = GameObject.FindWithTag("Vcam");
        
        if (start != null && finish != null && _camera != null)
        {
            _startFall = start.gameObject;
            _endFall = finish.gameObject;
            _vcam = _camera.GetComponent<CinemachineVirtualCamera>();
            _cinemachineFramingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else
            Debug.LogError("Triggers of FALLING ZONE not found!");
    }


    public void OnStartFalling()
    {
        _screenYDefault = _cinemachineFramingTransposer.m_ScreenY;
        _ortSizeDefault = _vcam.m_Lens.OrthographicSize;

        _cinemachineFramingTransposer.m_ScreenY = _screenY;
        _vcam.m_Lens.OrthographicSize = _ortSize;

    }

    public void OnFinishFalling()
    {
        _cinemachineFramingTransposer.m_ScreenY = _screenYDefault;
        _vcam.m_Lens.OrthographicSize = _ortSizeDefault;
    }

}

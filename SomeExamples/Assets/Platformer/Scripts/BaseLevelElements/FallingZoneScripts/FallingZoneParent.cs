using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingZoneParent : MonoBehaviour
{
    private GameObject _startFall, _endFall;


    [SerializeField]
    private Vector3 _offsetValue;
    private Vector3 _offsetDefaultValue;

    private CinemachineFreeLook _look;
    private CinemachineComposer _composer;
    private void Start()
    {
        
        Transform start = gameObject.transform.Find("Start");
        Transform finish = gameObject.transform.Find("Finish");
        GameObject _camera = GameObject.FindWithTag("MainCamera");
        if (start != null && finish != null && _camera != null)
        {
            _startFall = start.gameObject;
            _endFall = finish.gameObject;

            _look = _camera.GetComponent<CinemachineFreeLook>();
            _composer = _look.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
            _offsetDefaultValue = _composer.m_TrackedObjectOffset;
        }
        else
            Debug.LogError("Triggers of FALLING ZONE not found!");
    }


    public void OnStartFalling()
    {
        Debug.Log("Start falling");
        _composer.m_TrackedObjectOffset = _offsetValue;
    }

    public void OnFinishFalling()
    {
        Debug.Log("Finish falling");
        _composer.m_TrackedObjectOffset = _offsetDefaultValue;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterObject;
    private float _angle = 0;
    void Start()
    {
        InvokeRepeating("RotateSprites", 0.5f, 0.5f);
    }

    private void RotateSprites()
    {

        Debug.Log("Water rotated!");
        foreach(Transform waterItem in _waterObject.transform)
        {
            waterItem.localRotation *= Quaternion.Euler(0, 0, 180);

        }
        
    }

}

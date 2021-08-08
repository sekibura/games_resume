using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidButtonsScript : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        gameObject.SetActive(true);
#else
        gameObject.SetActive(false);
#endif
    }
}

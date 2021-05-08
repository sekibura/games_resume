using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnTextSystem:MonoBehaviour
{
    public static SpawnTextSystem Instance;

    [SerializeField]
    private Transform _textPrefab;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    

    public  void CreateText(string text, Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, position.y+1, position.z);
        GameObject textObj = Instantiate(_textPrefab, newPos, Quaternion.identity).gameObject;
        textObj.GetComponent<TMP_Text>().text = text;
        Destroy(textObj, 2);
    }

  

}

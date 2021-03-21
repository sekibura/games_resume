using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThisPlayer(collision))
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        //TODO
        //to next level!
        Debug.Log("Switch lvl");
    }

    private bool IsThisPlayer(Collider2D collider)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpPanelsManager : MonoBehaviour
{
    [SerializeField]
    private PopUpMessageWindow _popUpMessageWindow;

    public void OpenPopUpMessage(string message)
    {
        _popUpMessageWindow.gameObject.SetActive(true);
        _popUpMessageWindow.ShowMessage(message);
    }
}

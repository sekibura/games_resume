using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpPanelsManager : MonoBehaviour
{
    [SerializeField]
    private PopUpMessageWindow _popUpMessageWindow;
    [SerializeField]
    private PopUpMessageWindow _popUpMessageWindowHell;

    public void OpenPopUpMessage(string message)
    {
        _popUpMessageWindow.gameObject.SetActive(true);
        _popUpMessageWindow.ShowMessage(message);
    }
    public void OpenPopUpMessageHell(string message)
    {
        _popUpMessageWindowHell.gameObject.SetActive(true);
        _popUpMessageWindowHell.ShowMessage(message);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class MessageTableScript : MonoBehaviour
{
    public LocalizedString localizedString;
    string _message = "";
    [SerializeField]
    private PopUpPanelsManager _popUpPanelsManager;

    public void ShowMessage()
    {
        var localizedText = localizedString.GetLocalizedString();
        _message = localizedText;
        _popUpPanelsManager.OpenPopUpMessage(_message);
        
    }
}

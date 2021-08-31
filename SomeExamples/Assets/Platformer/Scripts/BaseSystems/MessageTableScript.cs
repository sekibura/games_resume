using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MessageTableScript : MonoBehaviour
{
    public LocalizedString localizedString;
    string _message = "";
    [SerializeField]
    private PopUpPanelsManager _popUpPanelsManager;
    [SerializeField]
    private bool _isHell = false;

    public void ShowMessage()
    {
        //LocalizationSettings.SelectedLocale = 
        var localizedText = localizedString.GetLocalizedString();
        _message = localizedText;
        if(_isHell)
            _popUpPanelsManager.OpenPopUpMessageHell(_message);
        else
            _popUpPanelsManager.OpenPopUpMessage(_message);
        
    }
}

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


    private void Start()
    {
        Debug.Log(LocalizationSettings.SelectedLocale.ToString());
        var localizedText = localizedString.GetLocalizedString();
        _message = localizedText;
        
    }
    public void ShowMessage()
    {
        //LocalizationSettings.SelectedLocale = 
        //var localizedText = localizedString.GetLocalizedString();
        //_message = localizedText;
        if(_message!="" && _message != null)
        {
            if (_isHell)
                _popUpPanelsManager.OpenPopUpMessageHell(_message);
            else
                _popUpPanelsManager.OpenPopUpMessage(_message);
        }
        
        
    }
}

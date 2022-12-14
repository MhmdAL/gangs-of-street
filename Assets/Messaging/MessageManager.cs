using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FlutterUnityPlugin;
using UnityEngine.EventSystems;

public class MessageManager : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] IconManager iconManager;
    [SerializeField] private Language defaultLanguage = Language.ar;

    Message msg = new Message();

    private void Start()
    {
#if UNITY_EDITOR
        // This part is typically done by flutter, but in the editor we init from unity.

        var langLetter = defaultLanguage == Language.ar ? "a" : defaultLanguage == Language.en ? "e" : "t";
        var message = new Message
        {
            data = langLetter
        };
        var messageString = JsonUtility.ToJson(message);

        InitConnection(messageString);
#endif
    }

    #region SendToFlutter

    public void SendMessageForIcon(string iconName)
    {
        Message msg = new Message();
        msg.data = iconName;
        msg.id = 1;
        Messages.Send(msg);
        Debug.Log(msg.data);

#if UNITY_EDITOR
        iconManager.AssignTowerPrize("5");
#endif
    }

    public void SendMessageForTower(string prize)
    {
        msg.data = prize;
        msg.id = 2;
        Messages.Send(msg);
        Debug.Log(msg.data);
    }

    #endregion

    #region GetFromFlutter

    public void InitConnection(string incoming)
    {
        Message message = Messages.Receive(incoming);
        if (message.data != null)
        {
            switch (message.data[0])
            {
                case 'a':
                    iconManager.AssignLanguage(Language.ar);
                    break;
                case 'e':
                    iconManager.AssignLanguage(Language.en);
                    break;
                case 't':
                    iconManager.AssignLanguage(Language.tr);
                    break;
            }


            message.data = "Conncetion Established correctly 200";
        }

        Messages.Send(message);
        
#if UNITY_EDITOR
        iconManager.AssignTowerPrize("10");
#endif
    }

    /// <summary> UPDATED
    // call in flutter and send string containg icon id seprated with , and time in seconds
    //exmple: SetTimer("0,60"); this will call the first icon and give it 60 seconds
    /// </summary>
    /// <param name="incoming"></param>
    public void SetTimer(string incoming)

    {
        Message message = Messages.Receive(incoming);
        if (message.data != null)
        {
            iconManager.AssignTimerPertiod(message.data);
        }
    }

    public void SetPrize(string prize)
    {
        Message message = Messages.Receive(prize);
        if (message.data != null)
        {
            iconManager.AssignTowerPrize(message.data);
        }
    }

    #endregion
}
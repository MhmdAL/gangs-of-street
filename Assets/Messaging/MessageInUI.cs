using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FlutterUnityPlugin;
using UnityEngine.EventSystems;

public class MessageInUI : MonoBehaviour,IEventSystemHandler
{
    [SerializeField] TMP_Text incomingMessageText;

    Message msg = new Message();
    // UnityMessageManager messageManager;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SendMessage()
    {
        // messageManager.SendMessageToFlutter("this message is from the Manager");
        msg.data = "this is from flutterPlugin";
        msg.id = 1;
        Messages.Send(msg);
        
        
    }
    public void FeedUnity(string incoming)
    {
        Message message = FlutterUnityPlugin.Messages.Receive(incoming);
        if (message.data != null)
           incomingMessageText.text = message.data;
           message.data = "Sending from unity";
           Messages.Send(message);

        // incomingMessageText.text = incoming;
    }
}

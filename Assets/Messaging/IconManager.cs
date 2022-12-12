using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using UnityEngine;
using System;
public class IconManager : MonoBehaviour
{
    IconData[] icons;
    [SerializeField] MessageManager messageManager;
    [SerializeField] PrizeBehaviour prizeBehaviour;
    // Start is called before the first frame update
    void Awake()
    {
        icons = GetComponentsInChildren<IconData>();
        foreach (IconData icon in icons)
            icon.iconManager = this;
        
    }
    public void AssignLanguage(Langugage lang)
    {
        foreach (IconData icon in icons)
            icon.SetLanguage(lang);
    }
    public void AssignTimerPertiod(string mesg) //updated
    {
        string[] msgData = mesg.Split(',');
        icons[Int32.Parse(msgData[0])].SetTime(float.Parse(msgData[1]));

        
    }
    public void IconClicked(string iconName)
    {
        messageManager.SendMessageForIcon(iconName);
    }

    public void AssignTowerPrize(string prize)
    {
        prizeBehaviour.AssignPrize(prize);
    }
    public void TowerClicked(string prize)
    {
        messageManager.SendMessageForIcon(prize);
    }
}

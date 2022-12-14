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

    private void Awake()
    {
        icons = GetComponentsInChildren<IconData>();
        foreach (IconData icon in icons)
            icon.iconManager = this;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var icons = FindObjectsOfType<IconData>();
            foreach (var icon in icons)
            {
                icon.SetCooldown(10);
            }

            var iconManager = FindObjectOfType<IconManager>();
            iconManager.AssignTowerPrize("-1");
        }
#endif
    }

    public void AssignLanguage(Language lang)
    {
        foreach (IconData icon in icons)
            icon.SetLanguage(lang);
    }

    public void AssignTimerPertiod(string mesg) //updated
    {
        string[] msgData = mesg.Split(',');
        icons[Int32.Parse(msgData[0])].SetCooldown(float.Parse(msgData[1]));
    }

    public void IconClicked(string iconName)
    {
        messageManager.SendMessageForIcon(iconName);
    }

    public void AssignTowerPrize(string prize)
    {
        Debug.Log(prize + "sending to unity");

        var timeTillNextPrize = int.Parse(prize);

        if (timeTillNextPrize == -1)
        {
            prizeBehaviour.SetPrize();
        }
        else if (timeTillNextPrize == 0)
        {
            prizeBehaviour.HidePrizeAndTimer();
        }
        else
        {
            prizeBehaviour.SetCooldown(timeTillNextPrize);
        }
    }

    public void TowerClicked()
    {
        Debug.Log("prize" + "sending from unity");

        messageManager.SendMessageForIcon("prize");
    }
}
using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;
public class PrizeBehaviour : MonoBehaviour
{
    [SerializeField] IconManager iconManager;
    [SerializeField] Image icon;
    [SerializeField] Sprite[] icons;
    [SerializeField] RTLTextMeshPro PrizeTextOnTower;
    string prize;
    // Start is called before the first frame update

    private void Awake()
    {
        icon.gameObject.SetActive(false);
        PrizeTextOnTower.text = "";
    }
    private void OnMouseUpAsButton()
    {
        Debug.Log(prize + "sending from unity");
        iconManager.TowerClicked(prize);
        icon.gameObject.SetActive(false);
        PrizeTextOnTower.text = "";
    }
    public void AssignPrize(string prize)
    {
        Debug.Log(prize + "sending to unity");
        string[] prizes = prize.Split(',');
        this.prize = prize;
        PrizeTextOnTower.text = prizes[1];
        int index = Int32.Parse(prizes[0]);
        if (icons.Length > index)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = icons[index];
        }
    }
}

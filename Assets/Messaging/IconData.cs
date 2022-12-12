
using RTLTMPro;
using UnityEngine;
public class IconData : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] string arabicName;
    [SerializeField] public string englishName;
    [SerializeField] string turkishName;
    [SerializeField] Sprite labelimage;
    [SerializeField] SpriteRenderer LablePlace; 
    [SerializeField] RTLTextMeshPro nameText;
    [HideInInspector] public IconManager iconManager;
    string nameInUse;

    //new code
    float timeRemaining = 10;
    public bool timerIsRunning = false;
    public RTLTextMeshPro timeText; 
    float timeRemainingDefault;

    public void SetTime(float val) //Updated
    {
        timeRemainingDefault = val;
        timeRemaining = val;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = timeRemainingDefault ;
                timerIsRunning = false;
                timeText.text = "";
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //end of new code

    void Start()
    {
        LablePlace.sprite = labelimage;
    }
    private void OnMouseUpAsButton() //updated
    {
       
            iconManager.IconClicked(id.ToString());        
        
    }

    public void SetLanguage(Langugage language)
    {
        if (language == Langugage.ar)
            nameInUse = arabicName;
        else if (language == Langugage.en)
            nameInUse = englishName;
        else if (language == Langugage.tr)
            nameInUse = turkishName;


        nameText.text = nameInUse;

    }
}
public enum Langugage
{
    ar, en, tr
}
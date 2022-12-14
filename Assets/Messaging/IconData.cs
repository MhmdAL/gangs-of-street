
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconData : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private int id;
    [SerializeField] private bool hasCooldown;
    [SerializeField] private Sprite labelimage;
    
    [SerializeField] private string arabicName;
    [SerializeField] private string englishName;
    [SerializeField] private string turkishName;
    [SerializeField] private string arabicAvailableString;
    [SerializeField] private string englishAvailableString;
    [SerializeField] private string turkishAvailableString;
    
    [SerializeField] private Color onCooldownColor = Color.red;
    [SerializeField] private Color availableColor = Color.green;

    [Header("Refs")]
    [SerializeField] private RTLTextMeshPro nameText;
    [SerializeField] private RTLTextMeshPro timeText; 
    
    [SerializeField] private Image timerOutlineImage; 

    [SerializeField] private SpriteRenderer LablePlace; 
    
    [HideInInspector] public IconManager iconManager;

    private Language _currentLanguage = Language.ar;

    private float _maxTime;
    private float _timeRemaining = 10;
    private bool _timerIsRunning = false;
    
    private void Start()
    {
        LablePlace.sprite = labelimage;

        UpdateCooldownDisplay();
    }
    
    private void Update()
    {
        if (_timerIsRunning && hasCooldown)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                
                _timerIsRunning = false;
            }

            UpdateCooldownDisplay();
        }
    }
    
    private void OnMouseUpAsButton() //updated
    {
        iconManager.IconClicked(id.ToString());
    }
    
    public void SetCooldown(float val) //Updated
    {
        if (!hasCooldown)
            return;

        _maxTime = val;
        _timeRemaining = val;
        _timerIsRunning = true;
    }
    
    public void SetLanguage(Language language)
    {
        _currentLanguage = language;
        
        nameText.text = GetName();

        UpdateCooldownDisplay();
    }

    private void UpdateCooldownDisplay()
    {
        if (!hasCooldown)
        {
            timeText.gameObject.SetActive(false);
            timerOutlineImage.transform.parent.gameObject.SetActive(false);
            return;
        }
        
        timeText.gameObject.SetActive(true);

        if (_timerIsRunning)
        {
            timerOutlineImage.transform.parent.gameObject.SetActive(true);

            timeText.color = onCooldownColor;

            timerOutlineImage.fillAmount = _timeRemaining / _maxTime;

            var adjustedTime = _timeRemaining + 1;
            float minutes = Mathf.FloorToInt(adjustedTime / 60);
            float seconds = Mathf.FloorToInt(adjustedTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerOutlineImage.transform.parent.gameObject.SetActive(false);

            timeText.color = availableColor;
            
            timeText.text = GetAvailableText();
        }
    }

    private string GetAvailableText()
    {
        if (_currentLanguage == Language.ar)
            return arabicAvailableString;
        else if (_currentLanguage == Language.en)
            return englishAvailableString;
        else if (_currentLanguage == Language.tr)
            return turkishAvailableString;

        return arabicAvailableString;
    }
    
    private string GetName()
    {
        if (_currentLanguage == Language.ar)
            return arabicName;
        else if (_currentLanguage == Language.en)
            return englishName;
        else if (_currentLanguage == Language.tr)
            return turkishName;

        return arabicName;
    }
}
public enum Language
{
    ar, en, tr
}
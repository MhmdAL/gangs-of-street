using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizeBehaviour : MonoBehaviour
{
    [SerializeField] private IconManager iconManager;

    [SerializeField] private Image prizeIconImage;
    [SerializeField] private RTLTextMeshPro timerText;
    
    private bool _hasPrize;
    private bool _isActive;
    private float _timeTillNextPrize;
    
    private void Update()
    {
        if (!_isActive || _hasPrize)
            return;

        _timeTillNextPrize -= Time.deltaTime;

        if (_timeTillNextPrize <= 0)
        {
            SetPrize();
        }
        
        UpdateCooldownDisplay();
    }

    private void OnMouseUpAsButton()
    {
        if (!_isActive || !_hasPrize)
            return;

        iconManager.TowerClicked();
    }

    private void UpdateCooldownDisplay()
    {
        var adjustedTime = _timeTillNextPrize + 1;
        float minutes = Mathf.FloorToInt(adjustedTime / 60);
        float seconds = Mathf.FloorToInt(adjustedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void HidePrizeAndTimer()
    {
        _isActive = false;
        
        _hasPrize = false;
        
        prizeIconImage.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    public void SetPrize()
    {
        _isActive = true;
        
        _hasPrize = true;

        prizeIconImage.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
    }

    public void SetCooldown(float timeTillNextPrize)
    {
        _isActive = true;
        
        _hasPrize = false;

        prizeIconImage.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);

        _timeTillNextPrize = timeTillNextPrize;
    }
}
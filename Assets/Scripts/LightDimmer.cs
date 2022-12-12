using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDimmer : MonoBehaviour
{
    [SerializeField] private ParticleSystem.MinMaxCurve intensityInterval;

    [SerializeField] private ParticleSystem.MinMaxCurve intensity;
    
    private Light _light;

    private float _currentInterval;
    private float _timer;

    private float _targetIntensity;
    
    private void Start()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= _currentInterval)
        {
            SetIntensity();
        }

        var dir = Mathf.Sign(_targetIntensity - _light.intensity);

        _light.intensity += dir * 4f * Time.deltaTime;
    }

    private void SetIntensity()
    {
        var intensity = Random.Range(this.intensity.constantMin, this.intensity.constantMax);
        _targetIntensity = intensity;

        if (intensity <= 1f)
        {
            _light.intensity = 0f;
        }
        
        var interval = GetInterval();
        _currentInterval = interval;

        _timer = 0;
    }

    private float GetInterval()
    {
        return Random.Range(intensityInterval.constantMin, intensityInterval.constantMax);
    }
}
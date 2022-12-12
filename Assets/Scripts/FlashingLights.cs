using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlashingLights : MonoBehaviour
{
    [SerializeField] private List<Light> lights;
    [SerializeField] private float onDuration;
    [SerializeField] private float offDuration;

    private bool _lightsOn;
    
    private float _timer;

    private void Awake()
    {
        _lightsOn = lights.First().enabled;
    }
    

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_lightsOn && _timer >= onDuration)
        {
            _lightsOn = false;

            foreach (var light in lights)
            {
                light.enabled = false;
            }

            _timer -= onDuration;
        }else if (!_lightsOn && _timer >= offDuration)
        {
            _lightsOn = true;

            foreach (var light in lights)
            {
                light.enabled = true;
            }

            _timer -= offDuration;
        }
    }
}
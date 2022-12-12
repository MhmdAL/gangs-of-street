using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Airplane : MonoBehaviour
{
    public Vector3 Target { get; set; }
    
    public float Speed { get; set; }
    
    private bool _reached;

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(Target - transform.position);
    }

    public void FadeIn()
    {
        foreach (var mr in GetComponentsInChildren<MeshRenderer>())
        {
            var col = mr.material.color;
            col.a = 0f;
            mr.material.color = col;

            mr.material.DOFade(1f, 1f);
        }
    }

    public void FadeOut()
    {
        foreach (var mr in GetComponentsInChildren<MeshRenderer>())
        {
            var col = mr.material.color;
            col.a = 1f;
            mr.material.color = col;
            
            mr.material.DOFade(0, 1f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
    
    private void Update()
    {
        if (_reached)
            return;
        
        float distance = Vector3.Distance(transform.position, Target);

        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        }
        else
        {
            _reached = true;
            
            FadeOut();
        }
    }
}
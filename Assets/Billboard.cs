using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billboard : MonoBehaviour
{
    private Camera _mainCam;
    
    private void Awake()
    {
        _mainCam = Camera.main;
    }

    void LateUpdate() {
        Quaternion r1 = Quaternion.LookRotation(transform.position - _mainCam.transform.position, Vector3.up);
        Vector3 euler2 = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(r1.eulerAngles.x, euler2.y, euler2.z);
    }
}
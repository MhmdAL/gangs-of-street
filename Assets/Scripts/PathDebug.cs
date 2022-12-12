using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathDebug : MonoBehaviour
{
    private List<Transform> _waypoints = new List<Transform>();
    
    private void OnEnable()
    {
        _waypoints.Clear();
        
        _waypoints.AddRange(GetComponentsInChildren<Transform>());

        _waypoints.RemoveAt(0);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _waypoints.Count - 1; i++)
        {
            var from = _waypoints[i].position;
            var to = _waypoints[i + 1].position;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(from, to);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_waypoints[i].position, 1);
        }
    }
}
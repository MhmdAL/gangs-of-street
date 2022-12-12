using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathWaypoint : MonoBehaviour
{
    public List<Transform> neighbors = new List<Transform>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        
        foreach (var n in neighbors)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(n.position, 1);
        }
    }
}
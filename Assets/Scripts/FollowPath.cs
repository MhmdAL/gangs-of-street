using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public GameObject path;
    public float speed;

    private List<Transform> _waypointTransforms = new List<Transform>();
    private int _currentWaypoint;
    private int _nextWaypoint;

    private void Start()
    {
        _waypointTransforms.AddRange(path.GetComponentsInChildren<Transform>());

        _waypointTransforms.RemoveAt(0);

        if (Random.Range(0, 2) == 1)
        {
            _waypointTransforms.Reverse();
        }

        transform.position = _waypointTransforms[0].position;
    }

    private void Update()
    {
        if (_nextWaypoint == -1)
        {
            return;
        }

        var dir = _waypointTransforms[_nextWaypoint].position - transform.position;

        transform.position += dir.normalized * (speed * Time.deltaTime);

        if (dir.magnitude <= 1f)
        {
            _currentWaypoint = _nextWaypoint;
            _nextWaypoint = _waypointTransforms.Count > _nextWaypoint + 1 ? _nextWaypoint + 1 : -1;

            if (_nextWaypoint == -1)
                Destroy(gameObject, 3f);
            
        }
    }

}

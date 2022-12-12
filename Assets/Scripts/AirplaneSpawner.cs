using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

public class AirplaneSpawner : MonoBehaviour
{
    [SerializeField] private Airplane AirplanePrefab;
    [SerializeField] private float radius;
    [SerializeField] private float outerRadius;
    [SerializeField] private MinMaxCurve spawnInterval;
    [SerializeField] private MinMaxCurve planeAltitude;
    [SerializeField] private MinMaxCurve planeSpeed;
    [SerializeField] private float minDistanceBetweenStartEnd;

    private float _currentSpawnInterval;
    private float _spawnTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var icons = FindObjectsOfType<IconData>();
            foreach (var icon in icons)
            {
                icon.SetLanguage(Langugage.ar);
                icon.SetTime(5);
            }
        }
        
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _currentSpawnInterval)
        {
            _spawnTimer -= _currentSpawnInterval;
            _currentSpawnInterval = Random.Range(spawnInterval.constantMin, spawnInterval.constantMax);

            SpawnAirplane();
        }
    }

    private void SpawnAirplane()
    {
        var altitude = Random.Range(planeAltitude.constantMin, planeAltitude.constantMax);

        var pointA = transform.position + GenerateRandomPointInCircle(altitude);
        var pointB = transform.position + GenerateRandomPointInCircle(altitude);

        while (Vector3.Distance(pointA, pointB) < minDistanceBetweenStartEnd)
        {
            pointB = transform.position + GenerateRandomPointInCircle(altitude);
        }

        var airplane = Instantiate(AirplanePrefab, pointA, Quaternion.identity);

        airplane.Target = pointB;
        airplane.Speed = Random.Range(planeSpeed.constantMin, planeSpeed.constantMax);

        airplane.FadeIn();
    }

    private void OnDrawGizmosSelected()
    {
        DrawCircle(transform.position, radius, 1000, Color.green);
        DrawCircle(transform.position, outerRadius, 1000, Color.red);
    }

    private Vector3 GenerateRandomPointInCircle(float altitude)
    {
        // var point = Random.insideUnitCircle * radius;
        //
        // return new Vector3(point.x, altitude, point.y);

        // Choose the radii of the two circles

        // Generate a random number x between 0 and 1
        float p = Random.Range(0, 1f);

        // Calculate the point's distance from the center as R = R1 + x * (R2 - R1)
        float R = radius + p * (outerRadius - radius);

        // Generate two random numbers y1 and y2 between -1 and 1
        float angle = Random.Range(0, 2 * Mathf.PI);

        float x = Mathf.Cos(angle) * R;
        float y = Mathf.Sin(angle) * R;

        // Calculate the point's coordinates as (R * y1, R * y2)
        Vector2 point = new Vector2(x, y);

        return new Vector3(point.x, altitude, point.y);
    }

    public static void DrawCircle(Vector3 position, float radius, int segments, Color color)
    {
        // If either radius or number of segments are less or equal to 0, skip drawing
        if (radius <= 0.0f || segments <= 0)
        {
            return;
        }

        // Single segment of the circle covers (360 / number of segments) degrees
        float angleStep = (360.0f / segments);

        // Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
        // which are required by Unity's Mathf class trigonometry methods

        angleStep *= Mathf.Deg2Rad;

        // lineStart and lineEnd variables are declared outside of the following for loop
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            // Line start is defined as starting angle of the current segment (i)
            lineStart.x = Mathf.Cos(angleStep * i);
            lineStart.z = Mathf.Sin(angleStep * i);

            // Line end is defined by the angle of the next segment (i+1)
            lineEnd.x = Mathf.Cos(angleStep * (i + 1));
            lineEnd.z = Mathf.Sin(angleStep * (i + 1));

            // Results are multiplied so they match the desired radius
            lineStart *= radius;
            lineEnd *= radius;

            // Results are offset by the desired position/origin 
            lineStart += position;
            lineEnd += position;

            // Points are connected using DrawLine method and using the passed color
            Debug.DrawLine(lineStart, lineEnd, color);
        }
    }
}
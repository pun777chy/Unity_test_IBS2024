using System.Collections.Generic;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour
{
    // Static list to keep track of all instances of this script
    public static List<FindNearestNeighbour> instances = new List<FindNearestNeighbour>();

    // LineRenderer component to draw lines between nearest neighbors
    private LineRenderer lineRenderer;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Add LineRenderer component to the GameObject and set line width
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        // Add this instance to the list of instances
        instances.Add(this);
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Remove this instance from the list of instances
        instances.Remove(this);
    }

    // Called once per frame
    private void Update()
    {
        // Find the nearest neighbor and draw a line to it
        FindAndDrawNearestNeighbour();
       
    }

    // Method to find the nearest neighbor and draw a line to it
    private void FindAndDrawNearestNeighbour()
    {
        FindNearestNeighbour nearest = null;
        float minDistance = float.MaxValue;

        // Iterate through all instances to find the nearest one
        foreach (var instance in instances)
        {
            // Skip self
            if (instance == this) continue;

            // Calculate distance to the other instance
            float distance = Vector3.Distance(transform.position, instance.transform.position);
            if (distance < minDistance)
            {
                // Update the nearest instance if this one is closer
                minDistance = distance;
                nearest = instance;
            }
        }

        // If a nearest instance is found, draw a line to it
        if (nearest != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, nearest.transform.position);
        }
        // If no nearest instance is found, draw a line to self (redundant line)
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    // The number of vertices of the polygon (from the user)
    public int vertexCount = 3; // Default is 6, but can be changed in the Unity Inspector
    // Radius of the circle
    public float radius = 3f;
    // Array to store the positions of the points
    public Vector3[] positions;

    // Center array to store center points for the polygon
    public Vector2[] center;

    private LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        CreateShape();
    }

    void CreateShape()
    {
        // Initialize the positions array for the vertices of the polygon
        positions = new Vector3[vertexCount];

        // Step for evenly placing points on the circle
        float angleStep = 360f / vertexCount;

        // Calculate coordinates for each vertex
        for (int i = 0; i < vertexCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // If center is provided, adjust the positions based on the center
            if (center.Length > 0 && center.Length > i)
            {
                // Adjust both x and y coordinates for the vertex
                x += center[i].x;
                y += center[i].y;
            }
            else if (center.Length > 0 && center.Length == 1)
            {
                // If only one center value is provided, apply it to all vertices
                x += center[0].x;
                y += center[0].y;
            }

            // Set the position for the vertex
            positions[i] = new Vector3(x, y, 0);
        }

        // Set up the LineRenderer
        line.positionCount = positions.Length;
        for (int i = 0; i < positions.Length; i++)
        {
            line.SetPosition(i, positions[i]);
        }

        // Close the loop to form a polygon
        line.loop = true;

        // Set up rounded corners for the line
        line.numCornerVertices = 90;
        line.numCapVertices = 90;

        // Set the line width
        line.startWidth = 0.6f;
        line.endWidth = 0.6f;
    }
}

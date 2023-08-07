using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Start()
    {
        // Create and configure the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Get the position of the mouse in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 point0 = new Vector3(transform.position.x, transform.position.y, -5);
        Vector3 point1 = new Vector3(mousePosition.x, mousePosition.y, -5);


        // Set the positions of the line renderer
        lineRenderer.SetPosition(0, point0);
        lineRenderer.SetPosition(1, point1);
    }
}
    
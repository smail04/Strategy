using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera rcCamera;
    private Vector3 startPoint;
    private Vector3 cameraStartPoint;

    private void Update()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = rcCamera.ScreenPointToRay(Input.mousePosition);

        float distance;

        plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);

        if (Input.GetMouseButtonDown(2))
        {
            startPoint = point;
            cameraStartPoint = transform.position;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 offset = point - startPoint;
            transform.position = cameraStartPoint - offset; 
        }

        transform.Translate(0, 0, Input.mouseScrollDelta.y);
        rcCamera.transform.Translate(0, 0, Input.mouseScrollDelta.y);
    }
}

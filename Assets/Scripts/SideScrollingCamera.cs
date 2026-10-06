using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    public Transform trackedObject;
    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = 0f;
    public float smoothSpeed = 5f;

    public float minX = -0.86f;

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = trackedObject.position.x;
        cameraPosition.x = Mathf.Max(cameraPosition.x, minX);

        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;

        cameraPosition.x = MathF.Max(cameraPosition.x, minX);
        transform.position = cameraPosition;
    }

}
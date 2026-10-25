using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoResize : MonoBehaviour
{
    private Camera camera;

    public static CameraAutoResize instance;
    
    private void Start()
    {
        camera = GetComponent<Camera>();
        instance = this;
    }

    public static void ResizeTo(Vector3 position, Bounds bounds)
    {
        Debug.DrawLine(position, position + (Vector3.up * bounds.extents.y), Color.red, 100);
        Debug.DrawLine(position, position - (Vector3.up * bounds.extents.y), Color.red, 100);
        Debug.DrawLine(position, position + (Vector3.right * bounds.extents.x), Color.red, 100);
        Debug.DrawLine(position, position - (Vector3.right * bounds.extents.x), Color.red, 100);
        instance.transform.position = position - Vector3.forward;
        instance.camera.orthographicSize = bounds.extents.y;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Camera cam;

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(cam.orthographicSize == 8)
            {
                cam.orthographicSize = 12;
            }
            else if(cam.orthographicSize == 12)
            {
                cam.orthographicSize = 8;
            }
        }
    }
}

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
            if(cam.orthographicSize == 5)
            {
                cam.orthographicSize = 10;
            }
            else if(cam.orthographicSize == 10)
            {
                cam.orthographicSize = 5;
            }
        }
    }
}

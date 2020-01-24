using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public bool invert = false;
    public float mouseSensitivity = 8f;
    public GameObject cam;

    private Transform root;
    private Transform head;

    public float jaw;
    public float pitch;

    void Start()
    {
        root = transform.parent;  


        head = cam.transform;
        jaw = root.eulerAngles.y;
        pitch = cam.transform.eulerAngles.x;
    }

    void LateUpdate()
    {
       
        if (!invert)
        {
            jaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        else
        {
            jaw -= Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        jaw = jaw > 180f ? jaw - 360f : jaw < -180f ? jaw + 360f : jaw;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        

        root.rotation = Quaternion.Euler(new Vector3(0f, jaw, 0f));
        head.rotation = Quaternion.Euler(new Vector3(pitch, jaw, 0f));
    }
}
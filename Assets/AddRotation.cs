using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRotation : MonoBehaviour
{

    public float rotationSpeed = 2;
    public Vector3 rotationAxis = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotationAxis.x * rotationSpeed * Time.deltaTime,
            transform.rotation.eulerAngles.y + rotationAxis.y * rotationSpeed * Time.deltaTime,
            transform.rotation.eulerAngles.z + rotationAxis.z * rotationSpeed * Time.deltaTime);
    }
}

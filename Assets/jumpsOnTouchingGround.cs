using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpsOnTouchingGround : MonoBehaviour
{
    private const string groundLayerName = "Ground";
    Rigidbody rb;
    public float ForceToAdd = 50;
    public float timeBetweenJumps = 10;
    private float timerBetweenJumps = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.GetMask(groundLayerName))
        {
            rb.AddForce(0, ForceToAdd / Time.fixedDeltaTime, 0, ForceMode.Impulse);
            timerBetweenJumps = Time.time + timeBetweenJumps;
        }
    }
}
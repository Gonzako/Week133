using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermitentJump : MonoBehaviour
{
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
    void FixedUpdate()
    {
        if(timerBetweenJumps < Time.time)
        {
            rb.AddForce(0, ForceToAdd, 0);
            timerBetweenJumps = Time.time + timeBetweenJumps;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodyVerticalVelTracker : MonoBehaviour
{
    public float downThreshold = -1f, upThreshold = 0.5f;

    public static event Action onVelocityChangeUp;
    public static event Action onVelocityChangeDown;
    public static event Action onJump;
    public static event Action onPeak;

    bool checkForPeak;
    float previousYVel;
    Rigidbody rb;
    RigidbodyPlayerMovement rbMovement;

    // Start is called before the first frame update
    void Start()
    {
        rbMovement = GetComponent<RigidbodyPlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void callJump()
    {
        onJump?.Invoke();
        onVelocityChangeUp?.Invoke();
        checkForPeak = true;
    }

    private void FixedUpdate()
    {
        if(rb.velocity.y > upThreshold && previousYVel < upThreshold && !checkForPeak)
        {
            onVelocityChangeUp?.Invoke();
        } else if(previousYVel > downThreshold && rb.velocity.y < downThreshold)
        {
            onVelocityChangeDown?.Invoke();
        } else if((previousYVel < downThreshold || previousYVel > upThreshold) 
            && (rb.velocity.y < upThreshold && rb.velocity.y > downThreshold) && checkForPeak)
        {
            checkForPeak = false;
            onPeak?.Invoke();
        }
        previousYVel = rb.velocity.y;
    }

    private void OnEnable()
    {
        rbMovement.onPlayerJump += callJump;
    }

    private void OnDisable()
    {
        rbMovement.onPlayerJump -= callJump;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

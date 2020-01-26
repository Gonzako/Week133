using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpsOnTouchingGround : MonoBehaviour
{
    public static event Action<GameObject> onBlockLand;
    public static event Action<GameObject> onBlockJump;



    private const string groundLayerName = "Ground";
    Rigidbody rb;
    public float ForceToAdd = 50;
    public float timeOnGround = 3;
    private float timerBetweenJumps = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator jumpRoutine()
    {
        yield return new WaitForSeconds(timeOnGround);
        rb.AddForce(0, ForceToAdd / Time.fixedDeltaTime, 0, ForceMode.Impulse);
        onBlockJump?.Invoke(this.gameObject);
    }


    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            onBlockLand?.Invoke(this.gameObject);
            StartCoroutine(jumpRoutine());
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(PlayerEnviromentChecker))]
public class RigidbodyPlayerMovement : MonoBehaviour, IPlayerMovement
{
    public static event Action onPlayerJump;
    public MovementValues MoveSettings;
    public bool canAirControl = false;
    private PlayerEnviromentChecker checker;
    private IPlayerInput reader;
    private CapsuleCollider capCollider;
    private Rigidbody rb;
    private bool wishJump = false;

    private Vector3 inputVector;
    [SerializeField] private Vector3 forceToAdd;

    private Vector3 desiredVelocity;
    private float desiredSpeed;

    // Start is called before the first frame update
    void Start()
    {
        reader = GetComponent<IPlayerInput>();
        checker = GetComponent<PlayerEnviromentChecker>();
        rb = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        QueueJump();
        if (checker.IsGrounded)
        {
            Debug.Log("Moving on ground");
            GroundMove();
        }
        else if(canAirControl)
        {
            Debug.Log("Air move");
            AirMove();
        }
        AddForce();
    }

    private void AddForce()
    {
        Debug.Log(forceToAdd);
        rb.AddForce(forceToAdd, ForceMode.VelocityChange);
        forceToAdd = Vector3.zero;
    }

    private void AirMove()
    { 
        float accel;
        Vector3 wishDir = DesireDirection();
        float wishSpeed = MoveSettings.baseSpeed;

        wishDir.Normalize();

        if(Vector3.Dot(rb.velocity, wishDir) < 0)
        {
            accel = MoveSettings.airDecceleration;
        }
        else
        {
            accel = MoveSettings.airAcceleration;
        }
        
        if(!(reader.Forward || reader.Backwards) && (reader.Left || reader.Right))
        {
            if (wishSpeed > MoveSettings.sideStrafeSpeed) wishSpeed = MoveSettings.sideStrafeSpeed;
            accel = MoveSettings.sideStrafeAcceleration;
        }
        Accelerate(wishDir, wishSpeed/2, accel/2);
        
        
    }

    private void GroundMove()
    {
        if (!wishJump)
        {
            ApplyFriction();
        }

        inputVector = DesireDirection();
        inputVector.Normalize();
        desiredSpeed = MoveSettings.baseSpeed;

        Accelerate(inputVector, desiredSpeed,
            MoveSettings.runAcceleration);

        if (checker.OnSlope)
        {
            forceToAdd -= Vector3.Project
                (forceToAdd, checker.Hit.normal);
        }

        if (wishJump)
        {
            forceToAdd.y = MoveSettings.jumpSpeed;
            wishJump = false;
            onPlayerJump?.Invoke();
        }
    }

         
    private void Accelerate(Vector3 wishDir, float wishSpeed, float accel)
    {
        if (wishDir.magnitude > 0)
        {

            float addSpeed, accelSpeed, currentSpeed;

            currentSpeed = Vector3.Dot(rb.velocity, wishDir);
            addSpeed = wishSpeed - currentSpeed;
            if (addSpeed <= 0)
                return;

            accelSpeed = accel * Time.fixedDeltaTime * wishSpeed;
            if (accelSpeed >= addSpeed)
                accelSpeed = addSpeed;


            forceToAdd.x += wishDir.x * accelSpeed;
            forceToAdd.z += wishDir.z * accelSpeed; 
        }
    }

    private void ApplyFriction()
    {
        Vector3 vec = forceToAdd; // Equivalent to: VectorCopy();
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        /* Only if the player is on the ground then apply friction */
        if (checker.IsGrounded)
        {
            control = speed < MoveSettings.runDeacceleration ? MoveSettings.runDeacceleration : speed;
            drop = control * MoveSettings.friction * Time.fixedDeltaTime;
        } 
        newspeed = speed - drop;

        if (speed > 0)
        {
            forceToAdd.x += -rb.velocity.x* 6f/MoveSettings.friction;
            forceToAdd.z += -rb.velocity.z* 6f/MoveSettings.friction;            
        }
        if (newspeed < 0)
        {
            forceToAdd.x = -rb.velocity.x;
            forceToAdd.z = -rb.velocity.z;
        }
      

    }

    private void QueueJump()
    {
        wishJump = false;

        if (MoveSettings.holdJumpToBhop)
        {
            wishJump = reader.JumpingHeld;
        }

        if (reader.JumpingPressed)
        {
            wishJump = true;
            reader.JumpingPressed = false;
        }
    }

    private Vector3 DesireDirection()
    {

        Vector3 result = new Vector3((reader.Right ? 1 : 0) + (reader.Left ? -1 : 0),
            0, (reader.Forward ? 1 : 0) + (reader.Backwards ? -1 : 0));
        result = transform.TransformDirection(result);
        return result;

    }

}

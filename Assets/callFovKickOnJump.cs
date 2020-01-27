using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callFovKickOnJump : MonoBehaviour
{
    public float kickStrengh = 10;
    public float kickTime = 0.2f, fallbackDur = 0.6f;

    fovKick fKick;
    // Start is called before the first frame update
    void OnEnable()
    {
        fKick = GetComponent<fovKick>();
        RigidbodyPlayerMovement.onPlayerJump += handleJump;
    }

    private void handleJump()
    {
        fKick.FovKick(kickStrengh, kickTime, fallbackDur);
    }

    private void OnDisable()
    {

        RigidbodyPlayerMovement.onPlayerJump -= handleJump;
    }
}

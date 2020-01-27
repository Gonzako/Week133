using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayerOnTouch : MonoBehaviour
{
    private const string playerLayer = "PlayerLayer";
    public static event Action onPlayerKill;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(playerLayer))
        {
            
            onPlayerKill?.Invoke();
        }
    }
}

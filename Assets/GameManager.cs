using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool playerMovementActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        playerMovementActive = true;
    }

    public void BlockInputs()
    {
        var a = GetPlayerMovementActive();
        if ( a == true)
        {
            playerMovementActive = false;
        }
        else
        {
            playerMovementActive = true;
        }
    }
    
    public void UnblockInputs()
    {
        playerMovementActive = true;
    }
    
    public bool GetPlayerMovementActive()
    {
        return playerMovementActive;
    }
}

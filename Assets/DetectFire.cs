using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectFire : MonoBehaviour
{
    private FireAbility Observer;

    private void Start()
    {
        Observer = FindObjectOfType<FireAbility>();
        
        if (Observer != null)
        {
            Observer.RegisterObserver(this);
        }
        else
        {
            Debug.LogWarning("No FireObserver found in the scene!");
        }
    }

    private void OnDestroy()
    {
        if (Observer != null)
        {
            Observer.UnregisterObserver(this);
        }
    }
    
    public abstract void OnFireEvent();
}

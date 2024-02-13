using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToChange;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null) 
        {
            LoadScreenManager.instance.LoadScene(sceneToChange);
        }
    }
}

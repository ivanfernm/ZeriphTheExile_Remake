using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmisionDirectionTest : MonoBehaviour
{
    public Renderer Renderer;
    public GameObject Activator;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        Vector3 a =  new Vector3(transform.position.x - Activator.transform.position.x,
            transform.position.y - Activator.transform.position.y,
            transform.position.z - Activator.transform.position.z);

        Renderer.material.SetVector("_EmisiveDirection",a);
    }

  
}

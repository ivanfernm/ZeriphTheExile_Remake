using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawEffectControler : MonoBehaviour
{
    private Renderer _rd;
    private Material _material;
    private float y = 0;

    private void Awake()
    {
        _rd = gameObject.GetComponent<Renderer>();
        _material = _rd.material;
    }

    private void Update()
    {
        y = y + Time.deltaTime;
    }

    public void ActiveState(bool state)
    {
        gameObject.SetActive(state);
    }

    public void UpdateMaterial(float a)
    {
        float current = Mathf.Lerp(1, -1, y);
        _material.SetFloat("_Disolve", current * a);
        
    }
    

}

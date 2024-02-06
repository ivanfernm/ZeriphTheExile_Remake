using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class HandFire : MonoBehaviour, IHeatEmmiter
{
    public float Heat { get; set; }

    public float intensity;
    public float Fixer = .1f;
    
    
    public VisualEffect visualEffect;
    public GameObject HandBone;

    public Light lightControl;
   
    void Start()
    {
        visualEffect = gameObject.GetComponentInChildren<VisualEffect>();

        //get the propertybinder and assign the hand bone to it
        
        visualEffect.SetVector3("Position", HandBone.transform.position);

        Heat = .5f;
        SetIntensity(Heat);

    }

    // Update is called once per frame

    public void SetIntensity(float intensityValue)
    {
        intensity = intensityValue * Fixer;
        visualEffect.SetFloat("Intensity", intensity);
        lightControl.intensity = lightControl.intensity * Mathf.Clamp(intensity,0,10);
    }
}

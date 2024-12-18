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
    public float Fixer = .01f;
    
    
    public VisualEffect visualEffect;
    public GameObject HandBone;

    public Light lightControl;
   
    void Start()
    {
        visualEffect = gameObject.GetComponentInChildren<VisualEffect>();

        //get the propertybinder and assign the hand bone to it
        Heat = .5f;
        SetIntensity(Heat);

    }

    // Update is called once per frame

    public void SetIntensity(float intensityValue)
    {
        intensity = intensityValue * Fixer;
        var intensityclamp = Mathf.Clamp(intensity, 0, 2f);
        lightControl.intensity = lightControl.intensity * intensityclamp;
       // Debug.Log("PreClamp Intensity: " + lightControl.intensity);
        lightControl.intensity = Mathf.Clamp(lightControl.intensity, 10000, 3000000f);
        //Debug.Log("PostClamp Intensity: " + intensity);

        
        visualEffect.SetFloat("Intensity", intensityclamp);

    }

    public void SetBone(Vector3 bonePosition)
    {
        transform.position = bonePosition;
    }
}

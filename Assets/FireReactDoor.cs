using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireReactDoor : DetectFire,IMelteable
{
    [SerializeField] IHeatEmmiter heatEmmiter;

    [SerializeField] private float CurrentTemperature;
    [SerializeField] private float ReactTemperature;
    [SerializeField] private float HeatReactSpeed;


    [Header("UI")]
    [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;


    [Header("VFx")]
    [SerializeField] private Material _material;

    [SerializeField] private GameObject _fireDetectionParticle;

    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get ; set; }
    public float MeltingSpeed { get; set; }
    public float currentTemperature { get ; set; }
    public bool IsMelted { get; set; }


    private void Awake()
    {
        _material = gameObject.GetComponentInChildren<Renderer>().material;
    }
    public void ReactToHeat(float heat)
    {
        if(CurrentTemperature >= ReactTemperature)
        {
            ActiveHeatBar.GetComponentInChildren<RadialBar>().SetFill(CurrentTemperature);
            Destroy(gameObject);
            return;
        }
        else
        {
            CurrentTemperature += heat * HeatReactSpeed;
            UpdateMaterial();
            ActiveHeatBar.GetComponentInChildren<RadialBar>().SetFill(CurrentTemperature);
            
        }
    } 

    public void SetHeatEmmiter(IHeatEmmiter heatEmmiter)
    {
        this.heatEmmiter = heatEmmiter;
    }

    private void OnTriggerEnter(Collider other)
    {
        var a = other.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            if (ActiveHeatBar == null)
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("DOOR");
            }
            

        }
    }

    private void OnTriggerStay(Collider other)
    {
        var a = other.GetComponentInChildren<IHeatEmmiter>();
        SetHeatEmmiter(a);

        if (a != null)
        {
            if (ActiveHeatBar == null)  
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("DOOR");
            }
            ReactToHeat(a.Heat);
        }
    }

    private void UpdateMaterial()
    {
        var oldHeat = 0f;
        var CurrentHeat = heatEmmiter.Heat / 100;
        var b = _material.GetFloat("_EmissiveExposureWeight");
        //Debug.Log(b);
        
        //I should store the heat value,and if the actual value is bigger we - but if its smaller we +

        if(CurrentHeat >= oldHeat )
        {
            b -= CurrentHeat;
            b = Mathf.Clamp(b, 0f, 1f);
            oldHeat = CurrentHeat;
            _material.SetFloat("_EmissiveExposureWeight", b);
        }

        else
        {
            b += CurrentHeat;
            b = Mathf.Clamp(b, 0f, 1f);
            oldHeat = CurrentHeat;
            _material.SetFloat("_EmissiveExposureWeight", b);
        }
    
    }

    public void Melt(IHeatEmmiter heatEmmiter)
    {
     
    }
    public void Cold()
    {
        //afeter the player stop using the flame the ice block will start to cool down
        currentTemperature -= 1 * MeltingSpeed;
        //update bar
        ActiveHeatBar.GetComponentInChildren<RadialBar>().SetFill(currentTemperature);
    }

    public override void OnFireEvent()
    {
        StartCoroutine(GlobalUtilities.ToggleObjectCoroutine(_fireDetectionParticle, 10f));
    }
}

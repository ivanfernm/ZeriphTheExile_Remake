using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReactTorch : MonoBehaviour, IMelteable
{
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
    public float currentTemperature { get; set; }
    public bool IsMelted { get; set; }

    [SerializeField] IHeatEmmiter heatEmmiter;

    [Header("UI")] [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;

    [Header("VFX")] [SerializeField] private GameObject TorchParticle;

    private void Start()
    {
        MeltingPoint = 100;
        MeltingSpeed = .5f;
        currentTemperature = 0;
        IsMelted = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        var a = other.gameObject.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            heatEmmiter = a;

            if (ActiveHeatBar == null && !IsMelted)
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("TORCH");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var a = other.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            if (ActiveHeatBar == null & !IsMelted)
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("TORCH");
                return;
            }

            Melt(a);
        }
    }

    public void Melt(IHeatEmmiter heatEmmiter)
    {
        if (currentTemperature >= MeltingPoint)
        {
            if (ActiveHeatBar != null)
            {
                ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
            }
            else return;
            LightTorch();
            return;
        }
        else
        {
            currentTemperature += heatEmmiter.Heat * MeltingSpeed;
            if (ActiveHeatBar != null)
            {
                ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
            }
            else return;
            
        }
    }

    public void Cold()
    {
        
    }

    private void LightTorch()
    {
        TorchParticle.gameObject.SetActive(true);
        IsMelted = true;
    }
}
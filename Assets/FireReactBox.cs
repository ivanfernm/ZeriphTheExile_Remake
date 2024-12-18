using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReactBox : DetectFire, IHeatEmmiter, IMelteable
{
    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
    public float currentTemperature { get; set; }

    public bool IsMelted { get; set; }
    public float Heat { get; set; }

    [SerializeField] float ActiveHeat;

    [SerializeField] IHeatEmmiter heatEmmiter;

    [Header("UI")] [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;

    [Header("VFX")] [SerializeField] private GameObject TorchParticle;
    [SerializeField] private GameObject _fireReactParticle;

    public void Melt(IHeatEmmiter heatEmmiter)
    {
        if (currentTemperature >= MeltingPoint)
        {
            CreateUI(currentTemperature);
            BurnBox();
            Heat = ActiveHeat;
            return;
        }
        else
        {
            currentTemperature += heatEmmiter.Heat * MeltingSpeed;
            CreateUI(currentTemperature);
        }
    }

    private void CreateUI(float currentTemp)
    {
        ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemp);
    }

    // Start is called before the first frame update
    void Start()
    {
        MeltingPoint = 100;
        MeltingSpeed = .8f;
        currentTemperature = 0;
        IsMelted = false;
        Heat = 0;
    }

    public override void OnFireEvent()
    {
        StartCoroutine(GlobalUtilities.ToggleObjectCoroutine(_fireReactParticle, 2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        var a = other.gameObject.GetComponentInChildren<IHeatEmmiter>();
        var b = other.gameObject.GetComponentInChildren<HandFire>();

        if (a != null && b != null)
        {
            heatEmmiter = a;

            if (heatEmmiter != null)
            {
                if (ActiveHeatBar ==null)
                {
                    var bar = Instantiate(HeatBar);
                    ActiveHeatBar = bar;
                    ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                    ActiveHeatBar.GetComponent<RadialBar>().SetName("BOX");
                }
                else
                {
                    Melt(a);
                }
              
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var a = other.GetComponentInChildren<IHeatEmmiter>();
        var b = other.gameObject.GetComponentInChildren<HandFire>();

        if (a != null && b != null)
        {
            if (ActiveHeatBar == null)
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("BOX");
            }

            Melt(a);
        }
    }


    private void BurnBox()
    {
        TorchParticle.gameObject.SetActive(true);
        IsMelted = true;
    }

    public void Cold()
    {
        if (currentTemperature > 0)
        {
            currentTemperature -= 1;
            ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
        }
    }

    //add a velocity that moves the box in the oposite direction of the playerm, for this we need a drag force, a ramping, and a direction vector.
}
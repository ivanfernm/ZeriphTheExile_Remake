using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReactWaterFount : DetectFire, IMelteable
{
    //objetives
    public HotWatterObjetive _Objetive;
    //task
    public HotWaterTask _Task;
    
    
    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
    public float currentTemperature { get; set; }
    public bool IsMelted { get; set; }

    [SerializeField] IHeatEmmiter heatEmmiter;

    [Header("UI")] [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;

    [Header("VFX")] [SerializeField] private GameObject TorchParticle;
    [SerializeField] private GameObject _fireDetectionParticle;

    void Start()
    {
        
        MeltingPoint = 100;
        MeltingSpeed = .5f;
        currentTemperature = 0;
        IsMelted = false;

        _Objetive = FindObjectOfType<HotWatterObjetive>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var a = other.gameObject.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            heatEmmiter = a;

            if (ActiveHeatBar == null && !IsMelted)
            {
                CreateUI();
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
                CreateUI();
                return;
            }

            Melt(a);
        }
    }

    public override void OnFireEvent()
    {
        throw new System.NotImplementedException();
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

            BoildWater();
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

    public void BoildWater()
    {
        Debug.Log("WatterIsBoiling");
        _Objetive.lvlobj.requiredTasks[0].;


    }

    public void Cold()
    {
        throw new System.NotImplementedException();
    }

    public void CreateUI()
    {
        var bar = Instantiate(HeatBar);
        ActiveHeatBar = bar;
        ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
        ActiveHeatBar.GetComponent<RadialBar>().SetName("Water");
        return;
    }
}
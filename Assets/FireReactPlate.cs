using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReactPlate : MonoBehaviour,IMelteable
{
    public enum active
    {
        active,
        desactive
    }
    
    
    [field: Header("Melting Properties")]
    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
    public float currentTemperature { get; set; }
    public bool IsMelted { get; set; }

    public bool tempIsIncreasing; 
    
    [SerializeField]public  IHeatEmmiter heatEmmiter;
    
    [Header("UI")]
    [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;
    
    [Header("VFx")]
    [SerializeField] private Material _material;


    [Header("Observers")] private List<IObserver> observers = new List<IObserver>();
    void Start()
    {
        MeltingPoint = 100;
        MeltingSpeed = .8f;
        currentTemperature = 0;
        IsMelted = false;
    }
    
    public void Melt(IHeatEmmiter heatEmmiter)
    {
        tempIsIncreasing = true;
        if (currentTemperature >= MeltingPoint)
        {
            ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
            Activate();
            return;
        }
        else
        {
            
            currentTemperature += heatEmmiter.Heat * MeltingSpeed;
            ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
        }
    }

    public void Cold()
    {
        tempIsIncreasing = false;
        if (currentTemperature > 0)
        {
            currentTemperature -= 1;
            currentTemperature = Mathf.Clamp(currentTemperature, 0, 100);
            ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
        }
    }
    
    public void Activate()
    {
      
        NotifyObservers();
    }
    
    private void OnTriggerEnter(Collider other)
    {

        var a = other.gameObject.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            heatEmmiter = a;

            if (heatEmmiter != null)
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

            if (ActiveHeatBar == null)
            {
                CreateUI();

            }
            Melt(a);
        }
        else
        {
            Cold();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Cold();
    }
    
    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify();
        }
    }
    private void CreateUI()
    {
        var bar = Instantiate(HeatBar);
        ActiveHeatBar = bar;
        ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
        ActiveHeatBar.GetComponent<RadialBar>().SetName("INTERUPTOR");
    }
}

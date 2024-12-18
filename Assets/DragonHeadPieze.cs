using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public enum DragHeadState
{
    low,
    mid,
    high,
    off
}

public class DragonHeadPieze : DetectFire, IMelteable
{
    public HeadPuzzleInterractor HeadPuzzleInterractor;
    
    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
    [SerializeField]public float currentTemperature { get; set; }
    public bool IsMelted { get; set; }
    
    [ContextMenu("debugcurrenttemp")]
    public void debugcurrenttemp(){Debug.Log(currentTemperature);}

    [SerializeField] IHeatEmmiter heatEmmiter;

    [Header("UI")] [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;

    [Header("VFX")] [SerializeField] private GameObject FirePuzzleParticle;
    [SerializeField] private GameObject _fireDetectionParticle;
    [SerializeField] private GameObject Light;


    [Header("Puzzle")] public DragHeadState DesiredPuzleTemp;
    public DragHeadState CurrentPuzleTemp = DragHeadState.off;
    public bool isInDesiredTemp;
    
    [Header("Colling")] 
    public float coldMultiplier = 1;
    public bool tempIsIncreasing;
    public float WaitingFColling = .2f;

    private void Start()
    {
        Light.SetActive(false);
        UpdatePuzzleTempState(currentTemperature);
        MeltingPoint = 100;
        MeltingSpeed = .5f;
        currentTemperature = 0;
        IsMelted = false;
        _fireDetectionParticle.SetActive(false);
    }

    public void resetTemperature()
    {
        if (ActiveHeatBar !=null)
        {
             Destroy(ActiveHeatBar);
        }
        FirePuzzleParticle.SetActive(false);
        MeltingPoint = 100;
        MeltingSpeed = .5f;
        currentTemperature = 0;
        IsMelted = false;
        _fireDetectionParticle.SetActive(false);
        UpdatePuzzleTempState(currentTemperature);
    }
    public void UpdatePuzzleTempState(float temp)
    {
        if (temp >= 10)
        {
            CurrentPuzleTemp = DragHeadState.low;
            StateActions(DragHeadState.low);

            FireTorch();

            if (temp >= 40)
            {
                CurrentPuzleTemp = DragHeadState.mid;
                StateActions(DragHeadState.mid);
                if (temp >= 75)
                {
                    CurrentPuzleTemp = DragHeadState.high;
                    StateActions(DragHeadState.high);
                }
            }
        }
        else
        {
            CurrentPuzleTemp = DragHeadState.off;
            StateActions(DragHeadState.off);
        }

        if (CurrentPuzleTemp == DesiredPuzleTemp)
        {
            isInDesiredTemp = true;
            HeadPuzzleInterractor.GetNotify();
        }
        else
        {
            isInDesiredTemp = false;
        }
    }

    private void StateActions(DragHeadState a)
    {
        switch (a)
        {
            case DragHeadState.low:
                var low = FirePuzzleParticle.GetComponent<VisualEffect>().GetVector4("LowTempColor");
                FirePuzzleParticle.GetComponent<VisualEffect>().SetVector4("CurrentColor", low);
                break;
            case DragHeadState.mid:
                var mid = FirePuzzleParticle.GetComponent<VisualEffect>().GetVector4("MidTempColor");
                FirePuzzleParticle.GetComponent<VisualEffect>().SetVector4("CurrentColor", mid);
                break;
            case DragHeadState.high:
                var high = FirePuzzleParticle.GetComponent<VisualEffect>().GetVector4("HighTempColor");
                FirePuzzleParticle.GetComponent<VisualEffect>().SetVector4("CurrentColor", high);
                break;
            case DragHeadState.off:
                FirePuzzleParticle.gameObject.SetActive(false);
                break;
        }
    }

    public override void OnFireEvent()
    {
        StartCoroutine(GlobalUtilities.ToggleObjectCoroutine(_fireDetectionParticle, 10f));
    }

    public void FireTorch()
    {
        FirePuzzleParticle.gameObject.SetActive(true);
        Light.SetActive(true);
        _fireDetectionParticle.SetActive(false);
        IsMelted = true;
    }

    public void Melt(IHeatEmmiter heatEmmiter)
    {
        tempIsIncreasing = true;
        if (currentTemperature >= MeltingPoint)
        {
            if (ActiveHeatBar != null)
            {
                ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
            }
            else return;

            return;
        }
        else
        {
            currentTemperature += heatEmmiter.Heat * MeltingSpeed;
            UpdatePuzzleTempState(currentTemperature);
            if (ActiveHeatBar != null)
            {
                ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
            }
            else return;
        }
    }
    
    private void CreateUI()
    {
        var bar = Instantiate(HeatBar);
        ActiveHeatBar = bar;
        bar.GetComponent<RadialBar>().itDestroyOnFull = false;
        ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
        ActiveHeatBar.GetComponent<RadialBar>().SetName("DragonHead");
    }

    IEnumerator WaitForCooling(float currentTemp)
    {
        while (currentTemp > 0 && tempIsIncreasing ==false)
        {
            
            Cold();
        
            // Yield to wait for next frame
            yield return null;
        }

        yield break;
    }
    
    public void Cold()
    {
        tempIsIncreasing = false;
        if (currentTemperature > 0)
        {
            currentTemperature -= 1 * coldMultiplier;
            currentTemperature = Mathf.Clamp(currentTemperature, 0, 100);
            UpdatePuzzleTempState(currentTemperature);
            ActiveHeatBar.GetComponent<RadialBar>().SetFill(currentTemperature);
          
        }
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

    private void OnTriggerExit(Collider other)
    {
        tempIsIncreasing = false;
        StartCoroutine(WaitForCooling(currentTemperature));
    }

    private void OnTriggerStay(Collider other)
    {
        var a = other.GetComponentInChildren<IHeatEmmiter>();

        if (a != null)
        {
            if (ActiveHeatBar == null & !IsMelted)
            {
                CreateUI();
            }

            Melt(a);
        }
    }
}
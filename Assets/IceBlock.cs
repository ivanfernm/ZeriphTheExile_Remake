using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour,IMelteable
{
    public float MeltingStartTemperature { get; set; }
    public float MeltingPoint { get; set; }
    public float MeltingSpeed { get; set; }
   [SerializeField]public float currentTemperature { get; set; }
   public bool IsMelted { get; set; }

   public float reactRadius; 

    private Material Material;

    [Header("UI")]
    [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameObject ActiveHeatBar;

    void Start()
    {
        MeltingPoint = 100;
        MeltingSpeed = .2f;
        currentTemperature = 0;

        Material = GetComponentInChildren<MeshRenderer>().material;
    }
    public void Melt(IHeatEmmiter a)
    {
       //melting speed is going to be based  in the player intensity flame intensity 
     
        if(currentTemperature == MeltingPoint)
        {
            Destroy(gameObject);
        }
        else
        {
            currentTemperature += a.Heat * MeltingSpeed;
            currentTemperature = Mathf.Clamp(currentTemperature, 0, MeltingPoint);
            ActiveHeatBar.GetComponentInChildren<RadialBar>().SetFill(currentTemperature);

            if (currentTemperature >= MeltingPoint)
            {
                Destroy(gameObject);
            }

        }
    }

    public void Cold()
    {
        //afeter the player stop using the flame the ice block will start to cool down
        currentTemperature -= 1 * MeltingSpeed;
        //update bar
        ActiveHeatBar.GetComponentInChildren<RadialBar>().SetFill(currentTemperature);
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
                ActiveHeatBar.GetComponent<RadialBar>().SetName("ICE");

            }


        }
    }
    private void OnTriggerStay(Collider other)
    {
        var  a =  other.GetComponentInChildren<IHeatEmmiter>();
        if (a != null)
        {

            if (ActiveHeatBar == null)
            {
                var bar = Instantiate(HeatBar);
                ActiveHeatBar = bar;
                ActiveHeatBar.transform.SetParent(CanvaManager.instance.HeatMenuPannel.transform, false);
                ActiveHeatBar.GetComponent<RadialBar>().SetName("ICE");
            }
            if (currentTemperature  ==  MeltingPoint)
            {
                return;
            }
            

            Melt(a);
        }
    }

    public void Melt()
    {
        throw new System.NotImplementedException();
    }
}
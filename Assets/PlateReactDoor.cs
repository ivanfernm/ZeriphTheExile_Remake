using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReactDoor : MonoBehaviour, IObserver
{
    public FireReactPlate fireReactPlate;

    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        fireReactPlate.RegisterObserver(this);
    }
    

    public void OnNotify()
    {
        ChangeDoorState();
    }
    
    void openDoor()
    {
        isActivated = true;
        gameObject.SetActive(false);
        Debug.Log("Door is open");
    }
    
    void closeDoor()
    {
        isActivated = false;
        gameObject.SetActive(true);
        Debug.Log("Door is closed");    
    }
    
    void ChangeDoorState()
    {
        if (isActivated)
        {
            closeDoor();
        }
        else
        {
            openDoor();
        }
    }
}

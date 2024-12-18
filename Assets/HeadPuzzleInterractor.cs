using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPuzzleInterractor : MonoBehaviour
{
    public List<DragonHeadPieze> Heads = new List<DragonHeadPieze>();
    public int count = 0;
    public int headsAmounnt = 4;

    private void Start()
    {
        Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            resetPuzzle();
        }
    }

    public void resetPuzzle()
    {
        foreach (var HP in Heads)
        {
            HP.resetTemperature();
            
        }
    }

    public void GetNotify()
    {
        CheckIfOpen();
    }

    private void CheckIfOpen()
    {
        var a = 0;
        foreach (var HP in Heads)
        {
            if ( HP.isInDesiredTemp == true)
            {
                a = a + 1;
            }
            
        }
        
        if (a >= headsAmounnt)
        {
            Debug.Log("AAAAAAAAAAAAAA");
                
            Open();
        }

        count = a;

    }

    private void Open()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void Close()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}

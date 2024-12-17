using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RadialBar : MonoBehaviour
{
    public Image barImage;
    public float currentfill;
    public float maxFill;
    public bool itDestroyOnFull = true;

    //text mesh pro text 
    public TextMeshProUGUI text;    
    

    private void Start()
    {
        currentfill = 0;
        maxFill = 100;
        SetFill(currentfill);   
    }

    [ContextMenu("Updatefill")]
    private void UpdateFill() 
    {
        currentfill = Mathf.Clamp(currentfill, 0, maxFill);
        barImage.fillAmount = currentfill / maxFill;
        CheckIfFull();
    }

    public void SetFill(float fill)
    {
        currentfill = fill;
        currentfill = Mathf.Clamp(fill, 0, maxFill);
        UpdateFill();
    }

    private void CheckIfFull()
    {
        if (itDestroyOnFull == true && currentfill >= maxFill)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetName(string name)
    {
        text.text = name;
    }
}

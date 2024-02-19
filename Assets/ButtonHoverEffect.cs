using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class ButtonHoverEffect : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Image buttonImage;

    
    void Start()
    {
     
        if (buttonImage == null)
        {
            Debug.LogError("ButtonHoverEffect: No Image component found on the button.");
        }
    }

    public void OnHoverEnter()
    {
        if (buttonImage != null) buttonImage.color = hoverColor;
    }

    public void OnHoverExit()
    {
        if (buttonImage != null) buttonImage.color = normalColor;
    }
    
}
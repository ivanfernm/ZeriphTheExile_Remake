using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition; // HDRP namespace

public class FireAbility : MonoBehaviour
{
    //obserservable class 
    private HashSet<DetectFire> _observers = new HashSet<DetectFire>(); 
    
    //
    
    public GameObject fireEffectPrefab; // Assign your fire particle effect prefab here
    [SerializeField]private GameObject currentFireEffect;
    public float intensityChangeRate = 0.5f;

    [SerializeField] private bool isOnFire;
    [SerializeField] private GameObject boneHand;

    [SerializeField] private Animator playerAnimator; 

    public float Maxfire = 10;// Rate at which light intensity changes

    void Update()
    {
        // Toggle fire creation
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFire();
        }

        // Adjust light intensity and particle size
        if (currentFireEffect != null ) // Middle mouse button held
        {
            AdjustFireProperties();
        }
        
        
      
    }

    void ToggleFire()
    {
        if (currentFireEffect == null)
        {
            // Create fire effect
            currentFireEffect = Instantiate(fireEffectPrefab, boneHand.transform.position, Quaternion.identity);
            currentFireEffect.GetComponent<HandFire>().HandBone = boneHand;
            currentFireEffect.transform.parent = boneHand.transform;
            
            float intensity = 1; // Set the initial intensity as needed
            var fire = currentFireEffect.GetComponentInChildren<HandFire>();
            fire.SetIntensity(intensity);
            isOnFire = true;
            playerAnimator.SetBool("IsTorching", isOnFire);
            ActivateFireEvent();

        }
        else
        {
            // Destroy the existing fire effect
            Destroy(currentFireEffect);
            isOnFire = false;
            playerAnimator.SetBool("IsTorching", isOnFire);
        }
    }


    void AdjustFireProperties()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Adjust HDRP light intensity

        if (scroll != 0)
        {
            var fire = currentFireEffect.GetComponentInChildren<HandFire>();
            if (scroll > 0f)
            {
                var a = Mathf.Clamp(fire.Heat += scroll * intensityChangeRate, 0, Maxfire);
                fire.SetIntensity(a);
                //Debug.Log("Scrolled Up" + a);
            }
            // Scroll down
            else if (scroll < 0f)
            {
                var a = Mathf.Clamp(fire.Heat += scroll * intensityChangeRate, 0, Maxfire);
                fire.SetIntensity(a);
               //Debug.Log("Scrolled Down" + a );
            }

        }
        
        //fireLight.intensity += scroll * intensityChangeRate;
        //fireLight.intensity = Mathf.Clamp(fireLight.intensity, 0, 10); // Adjust the min and max values as needed

        // Adjust particle size based on light intensity, clamping to double the original size
        //var main = fireParticles.main;
       // main.startSize = Mathf.Clamp(originalStartSize * fireLight.intensity, originalStartSize, originalStartSize * 2);
    }

    #region observable

    public void RegisterObserver(DetectFire observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void UnregisterObserver(DetectFire observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        //create list to avoid modification wile executing
        HashSet<DetectFire> observersCopy = _observers;

        foreach (var observer in observersCopy)
        {
            if (observer != null)
            {
                try
                {
                    observer.OnFireEvent();

                }
                catch
                {
                    Debug.LogError($"Error notifying observer ");
                }
                
            }
        }
    }

    public void ActivateFireEvent()
    {
        HashSet<DetectFire> observersCopy = new HashSet<DetectFire>(_observers);
        
        foreach (var observer in observersCopy)
        {
            if (observer != null)
            {
                try
                {
                    observer.OnFireEvent();
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error notifying observer {observer.name}: {e.Message}");
                }
            }
        }
    }
    

    #endregion
}
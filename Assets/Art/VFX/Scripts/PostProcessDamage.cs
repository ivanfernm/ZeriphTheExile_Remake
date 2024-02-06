using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessDamage : MonoBehaviour
{
    
    
    private Vignette _vignette;
    private VolumeProfile _volumeProfile;
    Volume _volume;
    

    // Start is called before the first frame update
    private void Awake()
    {
        _volumeProfile = GetComponent<UnityEngine.Rendering.Volume>().profile;
        _volumeProfile.TryGet<Vignette>(out _vignette);
        //EventManager.SubscribeToEvent(EventManager.EventsType.Player_HUD_Damange, GetDamage);
    }

    public void GetDamage(params object[] param)
    {
        //StartCoroutine(Lerp(0f, (float)param[1], (float)param[0]));
        //StartCoroutine(Lerp(0f, .3f, .5f));
    }
    public void GetDamage2()
    {StartCoroutine(Lerp(0f, .3f, .5f));}
    //create a function that lerp between two values in x amount of time
       public IEnumerator Lerp(float start, float end, float time)
        {
            float i = 0.0f;
            float rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                //lerp between start and end
                float lerp = Mathf.Lerp(start, end, i);
                //set the value of the material
                _vignette.intensity.value = lerp;

                StartCoroutine(inverseLerp(start, end, time));
                 
        

                yield return null;
            }
            
          
        }
       public IEnumerator inverseLerp(float start, float end, float time)
       {
           float i = 0.0f;
           float rate = 1.0f / time;
           while (i < 1.0f)
           {
               i += Time.deltaTime * rate;
               //lerp between start and end
               float lerp = Mathf.Lerp(end, start, i);
               //set the value of the material
               _vignette.intensity.value = lerp;
                 
                
 
               yield return null;
           }
       }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatEmissive : MonoBehaviour
{
    [SerializeField]LayerMask _ReactTo;
    [SerializeField] float _timeToReact;
    [SerializeField] Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.layer == 6 )
        {
            _renderer.material.SetFloat("_EmissiveIntensityUnit", 10);
        }
        else
        {
            _renderer.material.SetFloat("_EmissiveIntensityUnit", 0);
        }
    }
}

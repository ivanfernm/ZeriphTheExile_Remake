using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPlats : MonoBehaviour
{
    [SerializeField] List<GameObject> Plants;
    [SerializeField] List<Renderer> _renderers;

    private void Awake()
    {
        _renderers = new List<Renderer>();
       
        foreach (var plats in Plants)
        {
            var renderer = plats.GetComponentsInChildren<Renderer>();

            foreach (var item in renderer)
            {
                _renderers.Add(item);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var col = collision.gameObject;

        if (col != null)
        {
            foreach (var item in _renderers)
            {
                item.material.SetFloat("_EmissiveIntensity", 30);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        var col = collision.gameObject;
        if (col != null)
        {
            foreach (var item in _renderers)
            {
                item.material.SetFloat("_EmissiveIntensity", 8);
            }
        }
    }
}

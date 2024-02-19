using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour,IDragable,IPushable
{
    [SerializeField] Rigidbody _rg;
    public float pushForce = 5;

    private void Start()
    {
  
    }

    public void Drag(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void Push(Collision collision)
    {
        Vector3 pushDirection = collision.contacts[0].point - transform.position;
        pushDirection.y = 0; // Keep the push horizontal
        pushDirection.Normalize();

        // Apply the push force to this object
       // GetComponent<Rigidbody>().AddForce(-pushDirection * pushForce, ForceMode.Impulse);
       transform.position += -pushDirection * pushForce * Time.deltaTime;
       
    }

    public void StopPush()
    {
        throw new System.NotImplementedException();
    }


    private void OnCollisionEnter(Collision collision)
    {
        var a = collision.gameObject.GetComponent<DragMechanich>();

        if (a != null)
        {
            Push(collision);
            //Debug.Log("Player");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        var a = collision.gameObject.GetComponent<DragMechanich>();

        if (a != null)
        {
            Push(collision);
            //Debug.Log("Player");
        }
    }
}


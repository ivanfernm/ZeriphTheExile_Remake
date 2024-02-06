using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeltableDetector : MonoBehaviour
{

    public IMelteable target;

    private void OnTriggerEnter(Collider other)
    {
        var a  = other.GetComponentInChildren<IMelteable>();

        if (a != null )
        {
            target = a;

            var distaceToTarget = Vector3.Distance(other.gameObject.transform.position, transform.position);
            //Set the volume inntennsity up to .5 

            //Set the player emmisive based in distance
           //Debug.Log("somethinng meltable is close" + distaceToTarget);
        }    

    }

    private void OnTriggerExit(Collider other)
    {
        var a = other.GetComponentInChildren<IMelteable>();

        if (a != null)
        {
            target = a;

            //Set the volume inntennsity up to .5 

            //Set the player emmisive based in distance
            //Debug.Log("you went far from the object");
        }
    }
}

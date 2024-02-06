using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigibodyObjects : MonoBehaviour
{

    [SerializeField] private float pushPower = 2f;
    [SerializeField] private float targetMass;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) return;
        if (hit.moveDirection.y < -0.3F) return;

        targetMass = body.mass;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower / targetMass;
       
    }
}

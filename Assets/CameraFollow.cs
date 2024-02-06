using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float followSpeed = 10f;
    public float normalRotationSpeed = 5f;
    public float aimRotationSpeed = 10f;
    public FlameProjection flameProjectionScript;
    private float mouseX, mouseY;
    private bool isAiming;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //flameProjectionScript = player.GetComponent<FlameProjection>(); // Get the FlameProjection script from the player
    }

    private void LateUpdate()
    {
        isAiming = flameProjectionScript.isAiming;

        // Mouse input for rotation
        mouseX += Input.GetAxis("Mouse X") * (isAiming ? aimRotationSpeed : normalRotationSpeed);
        mouseY -= Input.GetAxis("Mouse Y") * (isAiming ? aimRotationSpeed : normalRotationSpeed);
        mouseY = Mathf.Clamp(mouseY, -35, 60); // Clamping vertical camera rotation

        Quaternion cameraTurnAngle = Quaternion.Euler(mouseY, mouseX, 0);
        Vector3 offsetPosition = cameraTurnAngle * offset;
        Vector3 desiredPosition = player.position + offsetPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        if (!isAiming)
        {
            // Standard camera behavior when not aiming
            transform.position = smoothedPosition;
            transform.LookAt(player.position);
        }
        else
        {
            // Different behavior when aiming
            //Make the camera look foward and capable of aiming based in the mouse position,make it follow the player and move around the player
            transform.position = smoothedPosition;
            transform.rotation = Quaternion.LookRotation(player.forward);
            transform.RotateAround(player.position, Vector3.up, mouseX);
        }
    }

}

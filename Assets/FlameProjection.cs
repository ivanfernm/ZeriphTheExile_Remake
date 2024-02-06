using UnityEngine;

public class FlameProjection : MonoBehaviour
{
    public LayerMask raycastLayers; // Assign the layers to be raycasted
    public GameObject flameProjectilePrefab; // Assign your flame projectile prefab here
    public Camera playerCamera; // Assign the main camera
    public bool isAiming = false; // A flag to indicate aiming state

    void Update()
    {
        // Check for aiming input
        if (Input.GetMouseButtonDown(1)) // Right mouse button for aiming
        {
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        // Launch flame projectile
        if (isAiming && Input.GetMouseButtonDown(0)) // Left mouse button for projection
        {
            LaunchFlameProjectile();
        }
    }

    void LaunchFlameProjectile()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, 100f, raycastLayers)) // Raycast with layer mask
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = (targetPoint - transform.position).normalized;

        GameObject projectile = Instantiate(flameProjectilePrefab, transform.position + Vector3.up, Quaternion.LookRotation(direction));
    }
}

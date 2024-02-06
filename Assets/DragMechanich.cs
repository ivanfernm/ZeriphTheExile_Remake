using UnityEngine;

public class DragMechanich : MonoBehaviour
{
    //public Camera mainCamera; // Assign this in the Inspector
    //public string interactButton = "Fire1"; // Default to left mouse button
    //private GameObject selectedObject;
    //private bool isDragging = false;

    //private void Start()
    //{
    //    if (mainCamera == null)
    //    {
    //        mainCamera = Camera.main;
    //    }
    //}

    //void Update()
    //{
    //    // Check if the interact button is pressed
    //    if (Input.GetButtonDown(interactButton))
    //    {
    //        RaycastHit hit;
    //        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    //        // Perform the raycast
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            // Check if the hit object can be dragged
    //            if (hit.collider != null && hit.collider.GetComponent<IDragable>() != null)
    //            {
    //                selectedObject = hit.collider.gameObject;
    //                isDragging = true;
    //            }
    //        }
    //    }

    //    // If dragging, update the position of the object
    //    if (isDragging && selectedObject != null)
    //    {
    //        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            // You can add an offset or specific logic for how the object follows the mouse
    //            selectedObject.transform.position = hit.point;
    //        }
    //    }

    //    // Check if the interact button is released
    //    if (Input.GetButtonUp(interactButton))
    //    {
    //        isDragging = false;
    //        selectedObject = null;
    //    }
    //}
}

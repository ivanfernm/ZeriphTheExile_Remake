using UnityEngine;
using System.Collections.Generic;
using System.Collections;


// Option 1: Static Utility Class
public static class GlobalUtilities
{
    // Static methods can be called without creating an instance
    public static float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2);
    }

    public static Vector3 GetRandomPointInCollider(Collider collider)
    {
        return collider.bounds.center + new Vector3(
            Random.Range(-collider.bounds.extents.x, collider.bounds.extents.x),
            Random.Range(-collider.bounds.extents.y, collider.bounds.extents.y),
            Random.Range(-collider.bounds.extents.z, collider.bounds.extents.z)
        );
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) > 0;
    }
    

    // Coroutine that handles the temporary object activation
    public static IEnumerator ToggleObjectCoroutine(GameObject targetObject, float duration)
    {
        // Ensure the object is turned on
        targetObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Turn the object off
        targetObject.SetActive(false);
    }
}
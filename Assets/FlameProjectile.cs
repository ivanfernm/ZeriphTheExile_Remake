using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 50f;
    private Vector3 launchPosition;

    void Start()
    {
        launchPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(launchPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Logic for what happens on collision (e.g., ignite, explode)
        Destroy(gameObject);
    }
}

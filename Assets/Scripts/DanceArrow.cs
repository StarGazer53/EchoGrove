using UnityEngine;

public class DanceArrow : MonoBehaviour
{
    [Header("Arrow Movement")]
    public float moveSpeed = 2.0f;         // Speed at which the arrow moves downward
    public float lifetime = 5.0f;          // Time before the arrow auto-destroys

    private void Start()
    {
        // Destroy the arrow after 'lifetime' seconds to clean up clones
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the arrow downward each frame along world Y-axis
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}

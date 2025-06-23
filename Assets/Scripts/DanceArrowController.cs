using UnityEngine;

public class DanceArrowController : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Vector3 moveDirection = Vector3.back; // Toward player (Z negative)

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Automatically clean up when it leaves the camera view
    }
}

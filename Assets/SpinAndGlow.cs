using UnityEngine;

public class SpinAndGlow : MonoBehaviour
{
    public float spinSpeed = 30f;
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}

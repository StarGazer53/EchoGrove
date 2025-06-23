using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DanceCueHitDetector : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty leftVelocityAction;
    public InputActionProperty rightVelocityAction;

    [Header("Feedback FX")]
    public ParticleSystem perfectFX;
    public ParticleSystem goodFX;
    public ParticleSystem missFX;
    public TextMeshProUGUI scorePopup;

    [Header("Thresholds")]
    public float perfectThreshold = 1.5f;
    public float goodThreshold = 0.8f;

    private int totalScore = 0;

    void Start()
    {
        if (leftVelocityAction != null) leftVelocityAction.action.Enable();
        if (rightVelocityAction != null) rightVelocityAction.action.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.name); // 🔍 Hit confirmation

        if (!other.CompareTag("DanceCue")) return;

        // Read velocities from both controllers
        Vector3 leftVel = leftVelocityAction.action.ReadValue<Vector3>();
        Vector3 rightVel = rightVelocityAction.action.ReadValue<Vector3>();

        float leftSpeed = leftVel.magnitude;
        float rightSpeed = rightVel.magnitude;

        float maxSpeed = Mathf.Max(leftSpeed, rightSpeed);

        // 🔍 Log movement values
        Debug.Log($"Trigger: {other.name}, Left Speed: {leftSpeed:F2}, Right Speed: {rightSpeed:F2}, Using: {maxSpeed:F2}");

        // Scoring logic
        if (maxSpeed >= perfectThreshold)
        {
            ShowFeedback("Perfect!", Color.green, perfectFX, 100);
        }
        else if (maxSpeed >= goodThreshold)
        {
            ShowFeedback("Good!", Color.yellow, goodFX, 70);
        }
        else
        {
            ShowFeedback("Miss", Color.red, missFX, 0);
        }
    }

    void ShowFeedback(string message, Color color, ParticleSystem fx, int scoreDelta)
    {
        if (fx != null) fx.Play();

        if (scorePopup != null)
        {
            scorePopup.text = message;
            scorePopup.color = color;
        }

        totalScore += scoreDelta;
        Debug.Log($"Score: {totalScore}");
    }
}

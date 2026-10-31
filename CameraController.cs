
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;
    public Transform ball;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 8f, -10f);
    public float smoothSpeed = 5f;
    public float lookHeight = 1.5f;

    void LateUpdate()
    {
        if (player == null) return;

        // Cible entre le joueur et le ballon
        Vector3 targetPosition = player.position;

        if (ball != null)
        {
            targetPosition = Vector3.Lerp(
                player.position,
                ball.position,
                0.25f
            );
        }

        Vector3 desiredPosition = targetPosition + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(
            targetPosition + Vector3.up * lookHeight
        );
    }
}

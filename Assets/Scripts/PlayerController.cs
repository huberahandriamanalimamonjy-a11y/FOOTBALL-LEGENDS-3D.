
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float rotationSpeed = 12f;

    [Header("Football")]
    public Transform ball;
    public float kickDistance = 2f;
    public float kickPower = 8f;

    private Rigidbody rb;
    private Vector3 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryKickBall();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            movement.x * moveSpeed,
            rb.linearVelocity.y,
            movement.z * moveSpeed
        );

        if (movement.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }

    void TryKickBall()
    {
        if (ball == null) return;

        float distance = Vector3.Distance(
            transform.position,
            ball.position
        );

        if (distance <= kickDistance)
        {
            BallController controller =
                ball.GetComponent<BallController>();

            if (controller != null)
            {
                Vector3 direction =
                    ball.position - transform.position;

                direction.y = 0.15f;

                controller.Kick(direction, kickPower);
            }
        }
    }
}


using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Ball Settings")]
    public float kickForce = 8f;
    public float maxSpeed = 20f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.43f;
        rb.linearDamping = 0.05f;
        rb.angularDamping = 0.05f;
    }

    public void Kick(Vector3 direction, float power)
    {
        Vector3 force = direction.normalized * power;
        rb.AddForce(force, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }
    }
}

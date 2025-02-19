using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pathParent; // Drag your "Path" GameObject here.
    public float moveForce = 5f;
    public float stopDistance = 0.5f;
    private Transform[] checkpoints;
    private int currentCheckpoint = 0;
    private Rigidbody rb;
    public float rotationSpeed = 360f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        // Build checkpoints array from the parent's children only.
        int count = pathParent.childCount;
        checkpoints = new Transform[count];
        
        for (int i = 0; i < count; i++)
        {
            checkpoints[i] = pathParent.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        if (currentCheckpoint >= checkpoints.Length)
            return; // Reached end of path

        Vector3 targetPosition = checkpoints[currentCheckpoint].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move the enemy using MovePosition
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveForce * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Rotate to face the direction of movement
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(0, 180, 0);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        if (Vector3.Distance(newPosition, targetPosition) < stopDistance)
        {
            currentCheckpoint++;
        }
    }
}
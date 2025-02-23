using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
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

            // Find the "Checkpoints" parent GameObject in the scene.
            GameObject checkpointsContainer = GameObject.Find("Checkpoints");
            if (checkpointsContainer == null)
            {
                Debug.LogError("Checkpoints container not found!");
                return;
            }

            // Get the parent's transform
            Transform cpParent = checkpointsContainer.transform;
            int count = cpParent.childCount;
            checkpoints = new Transform[count];

            // Loop through children in the order they appear in the Hierarchy.
            for (int i = 0; i < count; i++)
            {
                checkpoints[i] = cpParent.GetChild(i);
            }
        }

        void FixedUpdate()
        {
            if (checkpoints == null || currentCheckpoint >= checkpoints.Length)
                return; // No checkpoints or path complete

            Vector3 targetPosition = checkpoints[currentCheckpoint].position;
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Move enemy using MovePosition.
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveForce * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            // Calculate desired rotation with no X or Z rotation (only Y changes)
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(0, 180, 0);
                // Zero out x and z in the Euler angles.
                Vector3 targetEuler = targetRotation.eulerAngles;
                targetEuler.x = 0;
                targetEuler.z = 0;
                targetRotation = Quaternion.Euler(targetEuler);
        
                // Smoothly rotate using Slerp or RotateTowards.
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
                // Alternatively, you can try:
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            if (Vector3.Distance(newPosition, targetPosition) < stopDistance)
            {
                currentCheckpoint++;
            }
        }
    }
}

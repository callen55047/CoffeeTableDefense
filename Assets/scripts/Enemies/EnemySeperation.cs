using UnityEngine;

namespace Enemies
{
    public class EnemySeparation : MonoBehaviour
    {
        public float separationDistance = 1.5f; // Distance to start separating
        public float separationForce = 10f; // Force applied for separation

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Find all colliders within a sphere around this enemy
            Collider[] nearby = Physics.OverlapSphere(transform.position, separationDistance);
            Vector3 repulsion = Vector3.zero;

            foreach (Collider col in nearby)
            {
                // Make sure we're only repelling from other enemies (adjust tag if needed)
                if (col.gameObject != this.gameObject && col.CompareTag("RedEnemy"))
                {
                    Vector3 away = transform.position - col.transform.position;
                    float distance = away.magnitude;
                    if (distance > 0)
                    {
                        // Weight the force stronger when closer
                        repulsion += away.normalized / distance;
                    }
                }
            }

            if (repulsion != Vector3.zero)
            {
                // Apply a force in the repulsion direction
                rb.AddForce(repulsion * separationForce, ForceMode.Acceleration);
            }
        }
    }
}
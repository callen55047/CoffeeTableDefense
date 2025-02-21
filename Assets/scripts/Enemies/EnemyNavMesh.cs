using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyNavMesh : MonoBehaviour
    {
        [SerializeField] private GameObject destinationObject; // Assign the destination GameObject from the Hierarchy
        private NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            if (destinationObject != null)
            {
                // Log the destination object and its transform to verify it's correctly assigned.
                Debug.Log("Destination object assigned: " + destinationObject.name);
                Debug.Log("Destination transform: " + destinationObject.transform);
            
                agent.SetDestination(destinationObject.transform.position);
            }
            else
            {
                Debug.LogError("No destination object assigned to EnemyNavMesh.");
            }
        }

        void Update()
        {
            if (destinationObject != null)
            {
                // Continually update the destination in case it moves.
                if (agent.destination != destinationObject.transform.position)
                {
                    agent.SetDestination(destinationObject.transform.position);
                }
            }
        }
    }
}

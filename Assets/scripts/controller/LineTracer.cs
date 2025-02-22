using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class LineTracer : MonoBehaviour
{
    [Header("Line Settings")]
    public float lineLength = 1f;
    public Color lineColor = Color.red;

    [Header("Spawn Settings")]
    public GameObject spawnPrefab;

    private LineRenderer lineRenderer;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;
    private int playerLayer;

    void Start()
    {
        arRaycastManager = FindFirstObjectByType<ARRaycastManager>();
        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = lineColor };

        playerLayer = gameObject.layer; // Set the player layer to ignore during raycasts
    }

    void FixedUpdate()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * lineLength;

        // Update line positions
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        int layerMask = ~(1 << playerLayer); // Exclude player's layer
        Ray ray = new Ray(startPosition, transform.forward);

        if (arRaycastManager != null && arRaycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            foreach (var hit in hits)
            {
                Vector3 planeNormal = hit.pose.up;
                float verticalAlignment = Vector3.Dot(planeNormal, Vector3.up);

                if (Mathf.Abs(verticalAlignment) > 0.9f) // Horizontal plane
                {
                    Pose hitPose = hit.pose;

                    if (spawnedObject == null && spawnPrefab != null)
                    {
                        spawnedObject = Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
                        Debug.Log("Spawned object on horizontal plane.");
                    }
                    else if (spawnedObject != null)
                    {
                        spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }

                    return; // Stop after handling the first valid horizontal plane
                }
                else if (Mathf.Abs(verticalAlignment) < 0.1f) // Vertical plane
                {
                    if (spawnedObject != null)
                    {
                        Destroy(spawnedObject);
                        spawnedObject = null;
                        Debug.Log("Vertical plane hit. Destroyed spawned object.");
                    }
                    return;
                }
            }
        }

        // No valid planes found; destroy the object if it exists
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
            Debug.Log("No valid plane found. Destroyed spawned object.");
        }
    }
}
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public delegate void TransformUpdate(Transform spawnedTransform);

// create wrapper classes that implement this and provide the spawnPrefab
public class LineTracer : MonoBehaviour
{
    public event TransformUpdate onTransformOrNull;
    
    private GameObject spawnPrefab;
    private LineRenderer lineRenderer;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;
    private int playerLayer;
    private float lineLength = 1f;
    private OffsetData offsets = new OffsetData();

    public void setup(GameObject prefab)
    {
        spawnPrefab = prefab;
        arRaycastManager = FindFirstObjectByType<ARRaycastManager>();
        ConfigureLineRenderer();
        playerLayer = gameObject.layer; // Set the player layer to ignore during raycasts
    }

    public void addModifiers(OffsetData offsets)
    {
        this.offsets = offsets;
    }

    void FixedUpdate()
    {
        if (spawnPrefab == null) { return; }
        
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
                    spawnOrUpdateObject(hit.pose);
                    return;
                }
                else if (Mathf.Abs(verticalAlignment) < 0.1f) // Vertical plane
                {
                    DestroySpawnedObject();
                    return; // Exit after detecting a vertical plane
                }
            }
        }

        // No valid planes found
        DestroySpawnedObject();
    }

    public Transform getHitTransform()
    {
        return spawnedObject?.transform;
    }

    private void ConfigureLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0f;
        lineRenderer.endWidth = 0f;
    }
    
    private void NotifyListeners()
    {
        onTransformOrNull?.Invoke(spawnedObject?.transform);
    }

    private void spawnOrUpdateObject(Pose hitPose)
    {
        Quaternion finalRotation = hitPose.rotation * Quaternion.Euler(0f, offsets.rotation, 0f);
        Vector3 finalPosition = hitPose.position + (Vector3.up * offsets.height);

        if (spawnedObject == null && spawnPrefab != null)
        {
            spawnedObject = Instantiate(spawnPrefab, finalPosition, finalRotation);
        }
        else if (spawnedObject != null)
        {
            spawnedObject.transform.SetPositionAndRotation(finalPosition, finalRotation);
        }

        // TODO: scale is so small, it defaults to zero when modified...
        // if (offsets.scale != 0f)
        // {
        //     spawnedObject.transform.localScale.x *= offsets.scale;
        //     spawnedObject.transform.localScale.z *= offsets.scale;
        // }
        //
        // Debug.Log("object scale: " + spawnedObject.transform.localScale);
        
        NotifyListeners();
    }

    private void DestroySpawnedObject()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
            NotifyListeners();
        }
    }

    void OnDestroy()
    {
        // Cleanup the line renderer
        if (lineRenderer != null)
        {
            Destroy(lineRenderer);
        }

        // Cleanup the spawned object
        DestroySpawnedObject();
    }
}
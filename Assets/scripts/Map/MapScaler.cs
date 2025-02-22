using UnityEngine;

[ExecuteAlways]
public class MapScaler : MonoBehaviour
{
    // The fixed scale (width) you want every piece to have.
    private float fixedScale = 0.002941176f;

    private void OnValidate()
    {
        SetFixedScale();
    }

    private void Awake()
    {
        SetFixedScale();
    }

    private void SetFixedScale()
    {
        // Set the local scale uniformly
        transform.localScale = new Vector3(fixedScale, fixedScale, fixedScale);
    }
}
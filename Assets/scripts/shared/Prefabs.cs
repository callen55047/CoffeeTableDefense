using UnityEngine;

public class Prefabs
{
    public static GameObject BoardPlane()
    {
        GameObject prefab = Resources.Load<GameObject>("BoardPlane");
        if (prefab == null)
            Debug.LogWarning("Failed to load prefab. Ensure it's inside a 'Resources' folder.");
        
        return prefab;
    }
    
    public static GameObject GameBoardBase()
    {
        return Resources.Load<GameObject>("GameBoardBase");
    }
}
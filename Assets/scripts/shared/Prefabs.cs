using UnityEngine;

public class Prefabs
{
    public static GameObject BoardPlane()
    {
        GameObject prefab = Resources.Load<GameObject>("settings/BoardPlane");
        if (prefab == null)
            Debug.LogWarning("Failed to load prefab. Ensure it's inside a 'Resources' folder.");
        
        return prefab;
    }
    
    public static class World
    {
        public static GameObject gameboardOutline()
        {
            return Resources.Load<GameObject>("world/GameBoardOutline");
        }
    }

    public static class Gameboards
    {
        public static GameObject first()
        {
            return Resources.Load<GameObject>("boards/BoardBase1");
        }
    }
}
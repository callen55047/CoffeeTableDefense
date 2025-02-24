using UnityEngine;

public class Prefabs
{
    public static class Settings
    {
        public static GameObject BoardPlaceHolder()
        {
            return Resources.Load<GameObject>("settings/BoardPlaceHolder");
        }
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
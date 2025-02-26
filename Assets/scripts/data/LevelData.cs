using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string Name;               // Name of the level or entity
    public int DifficultyLevel;       // Difficulty level (1 = Easy, 2 = Medium, 3 = Hard)
    public GameObject LevelObject;    // Reference to a GameObject

    public LevelData(string name, int difficultyLevel, GameObject levelObject)
    {
        Name = name;
        DifficultyLevel = difficultyLevel;
        LevelObject = levelObject;
    }
}

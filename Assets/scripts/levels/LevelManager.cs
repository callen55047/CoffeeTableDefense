using UnityEngine;

public enum ELevelState
{
    NotStarted,
    Play,
    Paused,
    Stopped
}

public class LevelManager : MonoBehaviour
{
    private ELevelState levelState = ELevelState.NotStarted;

    public void Play()
    {
        // get transform of enemy spawn
        // spawn the enemySpawn object
        Debug.Log("level manager Play");
        levelState = ELevelState.Play;
    }

    public void Pause()
    {
        // set all AI characters to pause
        levelState = ELevelState.Paused;
    }

    public void StopPlay()
    {
        levelState = ELevelState.Stopped;
    }
}

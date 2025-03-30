using UnityEngine;
using UnityEngine.Animations;

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

    public void onMainGameState()
    {
        
    }

    public void onSettingsGameState()
    {
        
    } 

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

    public Transform GetEnemySpawnPoint()
    {
        // TODO: grab gameboard and get spawn point
        return transform;
    }

    public static LevelManager TransformSceneHandle()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            return gameManager.GetComponent<LevelManager>();
        }
        else
        {
            throw new System.Exception("GameObject LevelManager NOT FOUND!!!");
        }
    }
}

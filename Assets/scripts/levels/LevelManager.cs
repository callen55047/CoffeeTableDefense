using System.Collections.Generic;
using UnityEngine;

public enum ELevelState
{
    NotStarted,
    Play,
    Paused,
    Stopped
}

// this class will be attached to each playable level
public class LevelManager : MonoBehaviour
{
    [Header("Game Mode Settings")]
    public List<WaveData> Waves; 
    
    private ELevelState levelState = ELevelState.NotStarted;

    public void Start()
    {
        // subscribe to global events
        EventManager.OnMainGameState += onMainGameState;
        EventManager.OnSettingsGameState += onSettingsGameState;
        
        // get game object component for enemy spawn
    }

    public void Play()
    {
        Debug.Log("level manager Play");
        levelState = ELevelState.Play;
        
        // create enemy spawner(spawn point, Enemy spawn data) for each enemy in wave
        // calls back once enemies have run out
        // continue to next wave, or complete
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
    
    private void onMainGameState()
    {
        // keep in paused state until player decied to continue again
    }

    private void onSettingsGameState()
    {
        Pause();
    } 

    public static LevelManager fromScene()
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

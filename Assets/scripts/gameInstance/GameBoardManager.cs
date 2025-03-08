using UnityEngine;
using System;

public enum EGameBoardState
{
    NotCreated,
    Empty,
    Ready,
    Play
}

public class GameBoardManager : MonoBehaviour
{
    [Header("Game Maps")] 
    public LevelData[] levels;
    
    private EGameBoardState gameBoardState = EGameBoardState.NotCreated;
    private GameObject gameBoardBase;
    private LevelData selectedLevel;
	private GameObject currentLevel;
    
    public bool isPaused = false;
    
    public void onMainGameState()
    {
        // has the player set the initial location?
        if (GlobalValues.boardTransform == null)
        {
            gameBoardState = EGameBoardState.NotCreated;
            return;
        }

        // have we created the board yet?
        if (gameBoardBase == null)
        {
            gameBoardBase = Instantiate(
                Prefabs.World.gameboardOutline(),
                GlobalValues.boardTransform.position,
                GlobalValues.boardTransform.rotation
            );
			gameBoardBase.transform.localScale = GlobalValues.boardTransform.localScale;
            gameBoardState = EGameBoardState.Empty;
            
            // handle asset animation of popping up
            // animation will callback to this function
            onBaseInitialized();
            
            return;
        }
        
        // update board location
        gameBoardBase.transform.SetPositionAndRotation(
            GlobalValues.boardTransform.position, 
            GlobalValues.boardTransform.rotation
        );
		gameBoardBase.transform.localScale = GlobalValues.boardTransform.localScale;
        
        toggleVisibility(true);
        resume();
        handlePlayState();
    }

    public void onBaseInitialized()
    {
        if (levels.Length > 0)
        {
            spawnNewLevel(0);
            gameBoardState = EGameBoardState.Ready;
        }
    }

    public void spawnNewLevel(int index)
    {
        if (selectedLevel != null)
        {
            Destroy(selectedLevel.LevelObject);
        }
        
        selectedLevel = levels[index];
        currentLevel = Instantiate(
			selectedLevel.LevelObject, 
			gameBoardBase.transform.position, 
			gameBoardBase.transform.rotation
		);
		// debug scaling. 
		//levelSpawn.transform.localScale = GlobalValues.boardTransform.localScale;
		
    }
    
    public (int levelsLength, int selectedIndex) GetLevelsInfo()
    {
        int levelsLength = levels.Length;
        int selectedIndex = Array.IndexOf(levels, selectedLevel);  // Returns -1 if not found
        return (levelsLength, selectedIndex);
    }

    public void confirmPlayLevel()
    {
        // spawn new game mode instance and begin playing
		currentLevel.GetComponent<LevelManager>().Play();
    }

    public void onSettingsGameState()
    {
        pause();
        toggleVisibility(false);
    }
    
    public void resume()
    {
        // this affects player controller movement
        // Time.timeScale = 1;
        isPaused = false;
    }

    public void pause()
    {
        isPaused = true;
    }

    public bool isPlaying()
    {
        return gameBoardState == EGameBoardState.Play;
    }

    private void handlePlayState()
    {
        if (selectedLevel == null)
        {
            gameBoardState = EGameBoardState.Ready;
        }
        else
        {
            gameBoardState = EGameBoardState.Play;
        }
    }
    
    private void toggleVisibility(bool visible)
    {
        gameBoardBase?.SetActive(visible);
    }
    
    public static GameBoardManager fromScene() {
        GameObject gameManager = GameObject.Find("GameManager");
        
        if (gameManager != null) {
            GameBoardManager gameBoardManager = gameManager.GetComponent<GameBoardManager>();
            
            if (gameBoardManager != null) {
                return gameBoardManager;
            } else {
                throw new System.Exception("GameBoardManager script NOT FOUND!!!");
            }
        } else {
            throw new System.Exception("GameObject GameManager NOT FOUND!!!.");
        }
    }
}

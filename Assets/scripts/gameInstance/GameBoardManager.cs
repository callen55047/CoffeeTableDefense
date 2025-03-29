using UnityEngine;
using System;

public enum EGameBoardState
{
    NotCreated,
    Empty,
    // TODO: add Animating state,
    Ready
}

public class GameBoardManager : MonoBehaviour
{
    [Header("Game Maps")] 
    public LevelData[] levels;
    
    private EGameBoardState gameBoardState = EGameBoardState.NotCreated;
    private GameObject gameBoardBase;
    private LevelData selectedLevel;
	private GameObject currentLevel;
    
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
        
        visibleToPlayer(true);
        gameBoardState = EGameBoardState.Ready;
    }
    
    public void onSettingsGameState()
    {
        visibleToPlayer(false);
    }

    public void onBaseInitialized()
    {
        if (levels.Length > 0)
        {
            spawnNewLevel(0);
            
        }
    }

    public void spawnNewLevel(int index)
    {
        if (selectedLevel != null)
        {
            Destroy(selectedLevel.LevelObject);
            gameBoardState = EGameBoardState.Empty;
        }
        
        selectedLevel = levels[index];
        selectedLevel.LevelObject = Instantiate(
			selectedLevel.LevelObject, 
			gameBoardBase.transform.position, 
			gameBoardBase.transform.rotation
		);
        
        // TODO: add animation of level lifting out of ground
		// debug scaling. 
		//levelSpawn.transform.localScale = GlobalValues.boardTransform.localScale;
		
        // will be called from each gameBoardLevel
        onLevelSpawned();
    }

    public void onLevelSpawned()
    {
        gameBoardState = EGameBoardState.Ready;
    }
    
    public (int levelsLength, int selectedIndex) GetLevelsInfo()
    {
        int levelsLength = levels.Length;
        int selectedIndex = Array.IndexOf(levels, selectedLevel);  // Returns -1 if not found
        return (levelsLength, selectedIndex);
    }

    public bool IsReadyForPlay()
    {
        return gameBoardState == EGameBoardState.Ready;
    }
    
    private void visibleToPlayer(bool visible)
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
                throw new Exception("GameBoardManager script NOT FOUND!!!");
            }
        } else {
            throw new Exception("GameObject GameManager NOT FOUND!!!.");
        }
    }
}

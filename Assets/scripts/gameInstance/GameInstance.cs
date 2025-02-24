using UnityEngine;

public enum EGameState
{
    Init,
    Main,
    Settings
}

// TODO: create game mode and game state classes
// activate once we have a game board object on the map
public class GameInstance : MonoBehaviour
{
    private PlayerController playerController;
    private GameBoardManager gameBoardManager;
    private EGameState gameState = EGameState.Init;

    void Start()
    {
        playerController = PlayerController.fromScene();
        gameBoardManager = gameObject.GetComponent<GameBoardManager>();

        handleCurrentState();
    }

    public void changeState(EGameState newState)
    {
        gameState = newState;
        handleCurrentState();
    }

    private void handleCurrentState()
    {
        switch (gameState)
        {
            case EGameState.Init:
                preloadAssets();
                // add initial black screen for player
                // request access to camera for devices
                changeState(EGameState.Settings);

                break;
            case EGameState.Main:
                playerController.addBaseUI();
                gameBoardManager.onMainGameState();
                
                break;
            case EGameState.Settings:
                playerController.addSettingsUI();
                gameBoardManager.onSettingsGameState();
                
                break;
        }
    }

    private void preloadAssets()
    {
        // load all prefabs into memory
    }
    
    public static GameInstance fromScene() {
        GameObject gameManager = GameObject.Find("GameManager");
        
        if (gameManager != null) {
            GameInstance gameInstance = gameManager.GetComponent<GameInstance>();
            
            if (gameInstance != null) {
                return gameInstance;
            } else {
                throw new System.Exception("GameInstance script NOT FOUND!!!");
            }
        } else {
            throw new System.Exception("GameObject GameManager NOT FOUND!!!.");
        }
    }
}
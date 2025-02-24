using UnityEngine;

public enum EGameState
{
    Init,
    Main,
    Settings, 
    Play,
    Paused  
}

// TODO: create game mode and game state classes
// activate once we have a game board object on the map
public class GameInstance : MonoBehaviour
{
    private PlayerController playerController;
    private EGameState gameState = EGameState.Init;
    private GameObject gameBoard;

    void Start()
    {
        playerController = PlayerController.fromScene();

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
                handleGameBoardDisplay();
                
                break;
            case EGameState.Settings:
                playerController.addSettingsUI();
                handleGameBoardDisplay();
                
                break;
            case EGameState.Play:
                break;
            case EGameState.Paused:
                break;
        }
    }

    private void handleGameBoardDisplay()
    {
        // pause game and hide the current board if applicable
        // add separate render for the board position object
        if (GlobalValues.boardTransform != null)
        {
            gameBoard = Instantiate(
                Prefabs.World.gameboardOutline(),
                GlobalValues.boardTransform.position,
                GlobalValues.boardTransform.rotation
                );
        }
        else
        {
            if (gameBoard != null)
            {
                Destroy(gameBoard);
            }
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
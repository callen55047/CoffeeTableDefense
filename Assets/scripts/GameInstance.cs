using UnityEngine;

public enum EGameState
{
    Init,
    Main,
    Settings, 
    Play,
    Paused  
}

public class GameInstance : MonoBehaviour
{
    private PlayerController playerController;
    private EGameState gameState = EGameState.Init;
    private Transform boardTransform;
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
                changeState(EGameState.Main);

                break;
            case EGameState.Main:
                playerController.addBaseUI();
                boardTransform = playerController.completeLineTracer();
                handleGameBoardDisplay();
                
                break;
            case EGameState.Settings:
                playerController.addSettingsUI();
                // potentially call ARSession reset here
                playerController.addLineTracer();
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
        if (boardTransform != null)
        {
            gameBoard = Instantiate(
                Prefabs.Gameboards.first(),
                boardTransform.position,
                boardTransform.rotation
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
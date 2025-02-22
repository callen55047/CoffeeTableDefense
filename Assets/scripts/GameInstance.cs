using UnityEngine;

public enum EGameState
{
    Main,
    Settings, 
    Play,
    Paused  
}

public class GameInstance : MonoBehaviour
{
    private GameObject player;
    private PlayerController controller;
    private EGameState gameState = EGameState.Main;
    private GameObject gameBoard;

    void Start()
    {
        (player, controller) = PlayerController.fromScene();

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
            case EGameState.Main:
                controller.addBaseUI();
                Transform boardTransform = controller.completeLineTracer();
                handleGameBoardDisplay(boardTransform);
                
                break;
            case EGameState.Settings:
                controller.addSettingsUI();
                controller.addLineTracer();
                handleGameBoardDisplay(null);
                
                break;
            case EGameState.Play:
                break;
            case EGameState.Paused:
                break;
        }
    }

    private void handleGameBoardDisplay(Transform boardTransform)
    {
        if (boardTransform != null)
        {
            gameBoard = Instantiate(Prefabs.GameBoardBase(), boardTransform.position, boardTransform.rotation);
        }
        else
        {
            if (gameBoard != null)
            {
                Destroy(gameBoard);
            }
        }
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
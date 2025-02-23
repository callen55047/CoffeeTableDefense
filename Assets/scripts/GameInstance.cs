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
    [Header("Game Maps")]
    public GameObject map1;
    
    private GameObject player;
    private PlayerController controller;
    private EGameState gameState = EGameState.Main;
    private Transform boardTransform;
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
                boardTransform = controller.completeLineTracer();
                handleGameBoardDisplay();
                
                break;
            case EGameState.Settings:
                controller.addSettingsUI();
                // potentially call ARSession reset here
                controller.addLineTracer();
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
        if (boardTransform != null)
        {
            Vector3 worldSize = boardTransform.lossyScale;
            Debug.Log($"our world size if set to: x: {worldSize.x}, y: {worldSize.y}, z: {worldSize.z}");
            gameBoard = Instantiate(map1, boardTransform.position, boardTransform.rotation);
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
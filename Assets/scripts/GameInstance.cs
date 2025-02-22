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
                
                break;
            case EGameState.Settings:
                controller.addSettingsUI();
                
                break;
            case EGameState.Play:
                break;
            case EGameState.Paused:
                break;
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
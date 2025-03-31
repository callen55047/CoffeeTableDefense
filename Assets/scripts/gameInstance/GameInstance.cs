using UnityEngine;

public enum EGameState
{
    Init,
    Main,
    Settings
}

public class GameInstance : MonoBehaviour
{
    [Header("Editor Debug")] 
    public bool AutoPlaceBoard = true;
        
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
                
                #if UNITY_EDITOR
                    if (AutoPlaceBoard) {
                        createZeroBoardTransform();
                        changeState(EGameState.Main);
                        return;
                    }
                #endif
                
                // add initial black screen for player
                // request access to camera for devices
                changeState(EGameState.Settings);

                break;
            case EGameState.Main:
                playerController.addBaseUI();
                gameBoardManager.onMainGameState();
                EventManager.MainGameState();
                
                break;
            case EGameState.Settings:
                playerController.addSettingsUI();
                gameBoardManager.onSettingsGameState();
                EventManager.SettingsGameState();
                
                break;
        }
    }

    private void createZeroBoardTransform()
    {
        GameObject newObject = new GameObject("ZeroTransformObject");
        Transform zeroTransform = newObject.transform;
        
        zeroTransform.position = Vector3.zero;
        zeroTransform.rotation = Quaternion.identity;
        zeroTransform.localScale = Vector3.zero;

        GlobalValues.boardTransform = zeroTransform;
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
            throw new System.Exception("GameObject GameManager NOT FOUND!!!");
        }
    }
}
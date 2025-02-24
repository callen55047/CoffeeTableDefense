using UnityEngine;

public enum EGameBoardState
{
    NotCreated,
    Empty,
    Ready,
    Play,
    Paused  
}

public class GameBoardManager : MonoBehaviour
{
    private EGameBoardState gameBoardState = EGameBoardState.NotCreated;
    private GameObject gameBoard;
    
    public void onMainGameState()
    {
        // has the player set the initial location?
        if (GlobalValues.boardTransform == null)
        {
            gameBoardState = EGameBoardState.NotCreated;
            return;
        }

        // have we created the board yet?
        if (gameBoard == null)
        {
            gameBoard = Instantiate(
                Prefabs.World.gameboardOutline(),
                GlobalValues.boardTransform.position,
                GlobalValues.boardTransform.rotation
            );
            // handle asset animation of popping up
            gameBoardState = EGameBoardState.Empty;
            return;
        }
        
        // update board location
        gameBoard.transform.SetPositionAndRotation(
            GlobalValues.boardTransform.position, 
            GlobalValues.boardTransform.rotation
        );
        
        toggleVisibility(true);
        gameBoardState = EGameBoardState.Ready;
    }

    

    public void onSettingsGameState()
    {
        // ensure game is paused
        toggleVisibility(false);
    }
    
    public void resume()
    {
        
    }

    public void pause()
    {
        
    }
    
    private void toggleVisibility(bool visible)
    {
        gameBoard?.SetActive(visible);
    }
    
    void Update()
    {
        
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

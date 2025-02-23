using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("User Interface")]
    public GameObject baseUICanvas;
    public GameObject settingsUICanvas;

    private GameObject currentUICanvas;
    
    void Start()
    {
        
    }

    public void addBaseUI()
    {
        removeCurrentUI();
        
        currentUICanvas = Instantiate(baseUICanvas, transform);
        currentUICanvas.SetActive(true);
    }

    // have this function control adding the line tracer and updating the global value
    // then once complete, change the game state back to MAIN
    public void addSettingsUI()
    {
        removeCurrentUI();
        
        currentUICanvas = Instantiate(settingsUICanvas, transform);
        currentUICanvas.SetActive(true);
    }

    private void removeCurrentUI()
    {
        if (currentUICanvas != null)
        {
            currentUICanvas.SetActive(false);
            Destroy(currentUICanvas);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static PlayerController fromScene() {
        GameObject player = GameObject.Find("PlayerGO"); // GO = Game Object
        
        if (player != null) {
            PlayerController controller = player.GetComponent<PlayerController>();
            
            if (controller != null) {
                return controller;
            } else {
                throw new System.Exception("playerController script NOT FOUND!!!");
            }
        } else {
            throw new System.Exception("GameObject PlayerController NOT FOUND!!!.");
        }
    }
}

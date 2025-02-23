using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("User Interface")]
    public GameObject baseUICanvas;
    public GameObject settingsUICanvas;

    private GameObject currentUICanvas;
    private LineTracer tracer;
    
    void Start()
    {
        
    }

    public void addBaseUI()
    {
        removeCurrentUI();
        
        if (baseUICanvas != null)
        {
            currentUICanvas = Instantiate(baseUICanvas, transform);
            currentUICanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("Base UI Canvas not assigned.");
        }
    }

    public void addSettingsUI()
    {
        removeCurrentUI();
        
        if (settingsUICanvas != null)
        {
            currentUICanvas = Instantiate(settingsUICanvas, transform);
            currentUICanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("Settings UI Canvas not assigned.");
        }
    }
    
    public void addLineTracer()
    {
        tracer = gameObject.AddComponent<LineTracer>();
        // TODO: connect UI rotation and height sliders
        // tracer.onTransformOrNull += (transform) =>
        // {
        //     BoardManagerUI comp = settingsUICanvas.GetComponent<BoardManagerUI>();
        //     comp.setCanConfirm(transform != null);
        // };
    }

    public Transform completeLineTracer()
    {
        Transform tracerTransform = null;
        if (tracer != null)
        {
            tracerTransform = tracer.getHitTransform();
            Destroy(tracer);
        }
        
        return tracerTransform;
    }

    private void addCanvases()
    {
        if (baseUICanvas != null)
        {
            currentUICanvas = Instantiate(baseUICanvas, transform);
            currentUICanvas.SetActive(false);
        }
        
        if (settingsUICanvas != null)
        {
            currentUICanvas = Instantiate(settingsUICanvas, transform);
            currentUICanvas.SetActive(false);
        }
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

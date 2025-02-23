using UnityEngine;
using UnityEngine.UI;

public class BoardManagerUI : MonoBehaviour
{
    private Slider heightSlider;
    private Slider scaleSlider;
    private Slider rotationSlider;
    private Button confirmButton;
    
    private LineTracer tracer;
    
    void Start()
    {
        confirmButton = ChildFinder.getComponent<Button>(transform, "ConfirmBtn");
        confirmButton.onClick.AddListener(onConfirm);
        rotationSlider = ChildFinder.getComponent<Slider>(transform, "RotationSlider");
        rotationSlider.onValueChanged.AddListener(onRotationSliderChanged);
        
        setupLineTrace();
    }

    public void setCanConfirm(bool canConfirm)
    {
        // TODO: can't access UI button to show / hide
        if (confirmButton != null)
        {
            confirmButton.gameObject.SetActive(canConfirm);
        }
    }

    private void setupLineTrace()
    {
        // add to player game object
        tracer = PlayerController.fromScene().gameObject.AddComponent<LineTracer>();
        tracer.setup(Prefabs.BoardPlane());
        // TODO: connect UI rotation and height sliders
        // tracer.onTransformOrNull += (transform) =>
        // {
        //     BoardManagerUI comp = settingsUICanvas.GetComponent<BoardManagerUI>();
        //     comp.setCanConfirm(transform != null);
        // };
    }

    private void onRotationSliderChanged(float value)
    {
        tracer.addModifiers(value);
    }

    private void onConfirm()
    {
        if (tracer != null)
        {
            GlobalValues.boardTransform = tracer.getHitTransform();
            Destroy(tracer);
        }
        
        GameInstance
            .fromScene()
            .changeState(EGameState.Main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

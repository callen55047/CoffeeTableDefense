using UnityEngine;
using UnityEngine.UI;

public class BoardManagerUI : MonoBehaviour
{
    private Slider heightSlider;
    private Slider scaleSlider;
    private Slider rotationSlider;
    private Button confirmButton;
    
    private LineTracer tracer;
    private OffsetData offsets = new OffsetData();
    
    void Start()
    {
        confirmButton = ChildFinder.getComponent<Button>(transform, "ConfirmBtn");
        confirmButton.onClick.AddListener(onConfirm);
        rotationSlider = ChildFinder.getComponent<Slider>(transform, "RotationSlider");
        rotationSlider.onValueChanged.AddListener(onRotationSliderChanged);
        heightSlider = ChildFinder.getComponent<Slider>(transform, "HeightSlider");
        heightSlider.onValueChanged.AddListener(onHeightSliderChanged);
        scaleSlider = ChildFinder.getComponent<Slider>(transform, "ScaleSlider");
        scaleSlider.onValueChanged.AddListener(onScaleSliderChanged);
        
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
        tracer = gameObject.GetComponentInParent<PlayerController>().createLineTracer();
        tracer.setup(Prefabs.Settings.BoardPlaceHolder());
    }

    private void onRotationSliderChanged(float value)
    {
        offsets.rotation = value;
        tracer.addModifiers(offsets);
    }
    
    private void onHeightSliderChanged(float value)
    {
        offsets.height = value;
        tracer.addModifiers(offsets);
    }
    
    private void onScaleSliderChanged(float value)
    {
        Debug.Log("scale changed: " + value);
        offsets.scale = value;
        tracer.addModifiers(offsets);
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

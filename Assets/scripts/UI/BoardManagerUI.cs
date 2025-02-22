using UnityEngine;
using UnityEngine.UI;

public class BoardManagerUI : MonoBehaviour
{
    private Slider heightSlider;
    private Slider scaleSlider;
    private Slider rotationSlider;
    private Button confirmButton;
    
    void Start()
    {
        confirmButton = ChildFinder.getComponent<Button>(transform, "ConfirmBtn");
        confirmButton.onClick.AddListener(onConfirm);
    }

    private void onConfirm()
    {
        GameInstance
            .fromScene()
            .changeState(EGameState.Main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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

    public void setCanConfirm(bool canConfirm)
    {
        // TODO: can't access UI button to show / hide
        if (confirmButton != null)
        {
            confirmButton.gameObject.SetActive(canConfirm);
        }
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

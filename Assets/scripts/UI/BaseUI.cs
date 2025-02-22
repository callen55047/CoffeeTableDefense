using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    private Button myButton;

    void Start()
    {
        myButton = ChildFinder.getComponent<Button>(transform, "SettingsButton");
        myButton.onClick.AddListener(SettingsButtonClick);
    }
    
    void SettingsButtonClick()
    {
        GameInstance
            .fromScene()
            .changeState(EGameState.Settings);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    private Button settingsButton;
    private Button playButton;
    private Button selectRightButton;
    private Button selectLeftButton;

    private GameBoardManager manager;

    void Start()
    {
        settingsButton = ChildFinder.getComponent<Button>(transform, "SettingsButton");
        settingsButton.onClick.AddListener(SettingsButtonClick);
        
        // TODO: move these buttons to a new UI widget
        playButton = ChildFinder.getComponent<Button>(transform, "PlayButton");
        playButton.onClick.AddListener(PlayButtonClick);
        selectRightButton = ChildFinder.getComponent<Button>(transform, "SelectRight");
        selectRightButton.onClick.AddListener(RightButtonClick);
        selectLeftButton = ChildFinder.getComponent<Button>(transform, "SelectLeft");
        selectLeftButton.onClick.AddListener(LeftButtonClick);

        manager = GameBoardManager.fromScene();
    }
    
    void SettingsButtonClick()
    {
        GameInstance
            .fromScene()
            .changeState(EGameState.Settings);
    }

    void PlayButtonClick()
    {
        manager.confirmPlayLevel();
    }

    void RightButtonClick()
    {
        // move selection to the right
    }
    
    void LeftButtonClick()
    {
        // move selection to the right
    }

    void FixedUpdate()
    {
        setButtonVisibility();
    }

    private void setButtonVisibility()
    {
        var (length, index) = manager.GetLevelsInfo();
        if (length > 1 && index != -1)
        {
            if (index > 0)
            {
                selectLeftButton.gameObject.SetActive(true);
            }
            
            if (index + 1 < length)
            {
                selectRightButton.gameObject.SetActive(true);
            }
        }
        else
        {
            selectLeftButton.gameObject.SetActive(false);
            selectRightButton.gameObject.SetActive(false);
        }
    }
}
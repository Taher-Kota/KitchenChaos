using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI instance;
    [SerializeField] private Button resumeButton, mainMenuButton,optionsButton,tutorialButton;
    [SerializeField] private GameObject optionScreen,tutorialScreen;
    private bool gamePause;

    private void Awake()
    {
        instance = this; 
        Hide();

        resumeButton.onClick.AddListener(() => ToggleGamePause());
        mainMenuButton.onClick.AddListener(() => { 
            ToggleGamePause();
            Loader.LoadScenes(Loader.Scenes.MainMenu);
        });
        optionsButton.onClick.AddListener(() => { 
            optionScreen.SetActive(true);
            gameObject.SetActive(false);
        });
        tutorialButton.onClick.AddListener(() => {
            tutorialScreen.SetActive(true);
        });
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ToggleGamePause()
    {
        gamePause = !gamePause;
        if (gamePause)
        {
            Show();
            Time.timeScale = 0f;
        }
        else
        {
            Hide();
            Time.timeScale = 1f;
        }
        if (optionScreen.activeInHierarchy)
        {
            optionScreen.SetActive(false);
        }
    }

    public bool IsTutorialScreenShowing()
    {
        return tutorialScreen.activeInHierarchy;
    }
}

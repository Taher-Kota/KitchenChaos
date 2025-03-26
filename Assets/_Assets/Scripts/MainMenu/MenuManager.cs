using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton, quitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener( () => {
            Loader.LoadScenes(Loader.Scenes.LoadScene);
            });

        quitButton.onClick.AddListener( () =>
        {
            Application.Quit();
        });
    }
}

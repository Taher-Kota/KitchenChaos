using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image playingClockImage;
    private float GamePlayingTimer,Timer;

    private void Start()
    {
        GamePlayingTimer = GameManager.Instance.GetGamePlayingTimer();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            if (!playingClockImage.IsActive())
            {
                playingClockImage.gameObject.SetActive(true);
            }
            Timer += Time.deltaTime;
            playingClockImage.fillAmount = Timer/GamePlayingTimer;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public static GameManager Instance;
    public enum GameState
    {
        WaitingToStart, // for multiplayer
        StartCountDown,
        GamePlaying,
        GameOver
    }
    private GameState state;
    private float StartCountDownTimer = 3f;
    private float GamePlayingTimer = 180f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        state = GameState.WaitingToStart;
    }

    private void Start()
    {
        InputManager.Instance.OnGamePause += InputManager_OnGamePause;
        InputManager.Instance.onInteract += InputManager_onInteract;
    }

    private void InputManager_onInteract(object sender, EventArgs e)
    {
        if (state == GameState.WaitingToStart)
        {
            StartCoroutine(StartCountDown());
        }
    }

    private void InputManager_OnGamePause(object sender, EventArgs e)
    {
        GamePauseUI.instance.ToggleGamePause();
    }

    IEnumerator StartCountDown()
    {
        state = GameState.StartCountDown;
        yield return new WaitForSeconds(StartCountDownTimer);
        state = GameState.GamePlaying;
        StartCoroutine(GamePlaying());
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator GamePlaying()
    {
        yield return new WaitForSeconds(GamePlayingTimer);
        state = GameState.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePlaying()
    {
        return state == GameState.GamePlaying;
    }

    public bool IsCountDownStarted()
    {
        return state == GameState.StartCountDown;
    }

    public bool IsWaitingToStart()
    {
        return state == GameState.WaitingToStart;
    }

    public bool IsGameOver()
    {
        return state == GameState.GameOver;
    }

    public float GetCountDownTimer()
    {
        return StartCountDownTimer;
    }

    public float GetGamePlayingTimer()
    {
        return GamePlayingTimer;
    }
}

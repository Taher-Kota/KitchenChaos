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
    private float WaitingToStartTimer = 1f;
    private float StartCountDownTimer = 3f;
    private float GamePlayingTimer = 15f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        state = GameState.WaitingToStart;
    }

    private void Start()
    {
        StartCoroutine(WaitingToStart());
    }

    IEnumerator WaitingToStart()
    {
        yield return new WaitForSeconds(WaitingToStartTimer);
        state = GameState.StartCountDown;
        StartCoroutine(StartCountDown());
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator StartCountDown()
    {
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

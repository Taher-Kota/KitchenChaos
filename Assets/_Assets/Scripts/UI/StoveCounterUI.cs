using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Image bar;
    [SerializeField] private StoveCounter stoveCounter;

    private float maxTimer, timer = 0f;
    public State state = State.Idle;

    private void Start()
    {
        stoveCounter.StartUI += StoveCounter_StartUI;
    }

    private void StoveCounter_StartUI(object sender, StoveCounter.ProgressBarUIHandler e)
    {
        maxTimer = e.maxTimer;
        state = e.state;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                ResetProgress();
                break;
            case State.Frying:
                UpdateProgress();
                break;
            case State.Burned:
                UpdateProgress();
                break;
        }
    }

    private void ResetProgress()
    {
        timer = 0f;
        SetActiveOnce(false);
    }

    private void UpdateProgress()
    {
        SetActiveOnce(true);
        
        if (timer < maxTimer)
        {
            timer += Time.deltaTime;
            bar.fillAmount = Mathf.Clamp01(timer / maxTimer);

            if (bar.fillAmount == 1)
            {
                timer = 0f;
                if (state == State.Burned) state = State.Idle;
            }
        }
    }

    private void SetActiveOnce(bool isActive)
    {
        if (parent.activeSelf != isActive)
            parent.SetActive(isActive);
    }
}

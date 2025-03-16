using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownTxt;
    private float CountDownTimer;

    private void Start()
    {
        CountDownTimer = GameManager.Instance.GetCountDownTimer();
    }

    private void Update()
    {
        if (GameManager.Instance.IsCountDownStarted())
        {
            CountDownTimer -= Time.deltaTime;
            countDownTxt.text = CountDownTimer.ToString("F0");
            //countDownTxt.text = Mathf.Ceil(CountDownTimer).ToString();
            if(CountDownTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

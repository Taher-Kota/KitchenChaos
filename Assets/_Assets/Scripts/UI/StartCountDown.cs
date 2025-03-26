using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    private const string COUNT_DOWN_START_TRIGGER = "CountDownUI";
    [SerializeField] private TextMeshProUGUI countDownTxt;
    private float CountDownTimer;
    private Animator anim;
    private int previousCount;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        CountDownTimer = GameManager.Instance.GetCountDownTimer();
    }

    private void Update()
    {
        if (GameManager.Instance.IsCountDownStarted())
        {
            CountDownTimer -= Time.deltaTime;
            int currentcount = Mathf.CeilToInt(CountDownTimer);
            countDownTxt.text = currentcount.ToString();
            if (currentcount != previousCount)
            {
                anim.SetTrigger(COUNT_DOWN_START_TRIGGER);
                previousCount = currentcount;
                SoundManager.Instance.NumberPopUpSound();
            }
            if (CountDownTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurnWarningUI : MonoBehaviour
{
    private const string WARNING_BOOL = "StartWarning";
    [SerializeField] private StoveCounter stoveCounter;
    private Image warningImage;
    private Animator animator;
    private bool startWarning;
    private float maxSoundTimer, previousSoundTimer = .5f,redFlashWarningTimer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        warningImage = GetComponentInChildren<Image>();
        Hide();
    }

    private void Start()
    {
        stoveCounter.StartUI += StoveCounter_StartUI;
        stoveCounter.OnBurned += StoveCounter_OnBurned;
    }

    private void StoveCounter_StartUI(object sender, StoveCounter.ProgressBarUIHandler e)
    {
        if (e.state == State.Burned)
        {
            redFlashWarningTimer = e.maxTimer / 2;
            startWarning = true;
            Show();
        }
    }

    private void StoveCounter_OnBurned(object sender, System.EventArgs e)
    {
        startWarning = false;
        Hide();
    }

    private void Update()
    {
        if (startWarning)
        {
            maxSoundTimer -= Time.deltaTime;
            redFlashWarningTimer -= Time.deltaTime;

            if (maxSoundTimer <= 0f) {
                maxSoundTimer = previousSoundTimer - .05f;
                if (maxSoundTimer >= .15f) {
                    previousSoundTimer = maxSoundTimer;
                }
                SoundManager.Instance.PlayWarningSound(transform.position);
            }
            
            if(redFlashWarningTimer <= 0f)
            {
                warningImage.color = Color.red;
            }
        }
    }

    private void Show()
    {
        animator.SetBool(WARNING_BOOL, true);
        warningImage.enabled = true;
    }

    private void Hide()
    {
        warningImage.color = Color.white;
        previousSoundTimer = .5f;
        animator.SetBool(WARNING_BOOL, false);
        warningImage.enabled = false;
    }
}

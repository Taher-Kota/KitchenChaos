using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinkerManager : MonoBehaviour
{
    private Image buttonImage;
    private Coroutine blinkingCoroutine;

    public void StartBlinking(Image btnImage)
    {
        buttonImage = btnImage;
        blinkingCoroutine = StartCoroutine(StartBlinkingEffect());
    }

    public void StopBlinking()
    {
        if (blinkingCoroutine != null && buttonImage != null)
        {
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = null;
            buttonImage.CrossFadeAlpha(1f, 0f, true);
            buttonImage = null;
        }

    }

    IEnumerator StartBlinkingEffect()
    {
        while (true)
        {
            buttonImage.CrossFadeAlpha(0f, .2f, true);
            yield return new WaitForSecondsRealtime(.25f);
            buttonImage.CrossFadeAlpha(1f, .2f, true);
            yield return new WaitForSecondsRealtime(.25f);
        }
    }
}

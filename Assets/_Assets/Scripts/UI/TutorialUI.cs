using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpText, moveDownText, moveRightText, moveLeftText,
        interactText, interactAlternateText, pauseText;
    
    private void Awake()
    {
        Show();
    }

    private void InputManager_onInteract(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Start()
    {
       InputManager.Instance.onInteract += InputManager_onInteract;
       moveUpText.text = InputManager.Instance.GetBindingText(Binding.Move_Up);
       moveDownText.text = InputManager.Instance.GetBindingText(Binding.Move_Down);
       moveLeftText.text = InputManager.Instance.GetBindingText(Binding.Move_Left);
       moveRightText.text = InputManager.Instance.GetBindingText(Binding.Move_Right);
       interactText.text = InputManager.Instance.GetBindingText(Binding.Interact);
       interactAlternateText.text = InputManager.Instance.GetBindingText(Binding.InteractAlternate);
       pauseText.text = InputManager.Instance.GetBindingText(Binding.GamePause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider soundEffectSlider, musicSlider;
    [SerializeField] private TextMeshProUGUI soundEffectText,musicText;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button moveUpBtn,moveDownBtn,moveRightBtn, moveLeftBtn,interactBtn,interactAlternateBtn,pauseBtn;
    [SerializeField] private TextMeshProUGUI moveUpText,moveDownText,moveRightText,moveLeftText,interactText,interactAlternateText,pauseText;
    [SerializeField] private Image moveUpImg,moveDownImg,moveRightImg,moveLeftImg,interactImg,interactAlternateImg,pauseImg;
    [SerializeField] private ButtonBlinkerManager buttonBlinkerManager;
    private Button currentKeyBindingButton; // store key binding button that is currently being changed
    private void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            pauseScreen.SetActive(true);
            gameObject.SetActive(false);
        });

        soundEffectSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();

        UpdateKeyText();

        
        AssignButtonListener(moveUpBtn, moveUpText, Binding.Move_Up,moveUpImg);
        AssignButtonListener(moveDownBtn, moveDownText, Binding.Move_Down,moveDownImg);
        AssignButtonListener(moveLeftBtn, moveLeftText, Binding.Move_Left,moveLeftImg);
        AssignButtonListener(moveRightBtn, moveRightText, Binding.Move_Right,moveRightImg);
        AssignButtonListener(interactBtn, interactText, Binding.Interact,interactImg);
        AssignButtonListener(interactAlternateBtn, interactAlternateText, Binding.InteractAlternate,interactAlternateImg);
        AssignButtonListener(pauseBtn, pauseText, Binding.GamePause,pauseImg);
    }

    private void AssignButtonListener(Button button, TextMeshProUGUI text, Binding binding,Image btnImage)
    {
        button.onClick.AddListener(() =>
        {
            currentKeyBindingButton = button;
            text.text = ""; // Clear text while waiting for input
            buttonBlinkerManager.StartBlinking(btnImage);
            UpdateKeyBinding(binding);
        });
    }


    public void OnSoundEffectChange()
    {
        SoundManager.Instance.ChangeVolume(soundEffectSlider.value/100f);
        soundEffectText.text = "Sound Effect Volume : " + soundEffectSlider.value;
    }

    public void OnMusicChange()
    {
        MusicManager.Instance.ChangeVolume(musicSlider.value/100f);
        musicText.text = "Music Volume : " + musicSlider.value;
    }

    private void UpdateKeyText()
    {
        moveUpText.text = InputManager.Instance.GetBindingText(Binding.Move_Up);
        moveDownText.text = InputManager.Instance.GetBindingText(Binding.Move_Down);
        moveRightText.text = InputManager.Instance.GetBindingText(Binding.Move_Right);
        moveLeftText.text = InputManager.Instance.GetBindingText(Binding.Move_Left);
        interactText.text = InputManager.Instance.GetBindingText(Binding.Interact);
        interactAlternateText.text = InputManager.Instance.GetBindingText(Binding.InteractAlternate);
        pauseText.text = InputManager.Instance.GetBindingText(Binding.GamePause);
    }

    private void UpdateKeyBinding(Binding binding)
    {
        InputManager.Instance.RebindBinding(binding,StopBlinking);
    }

    private void StopBlinking()
    {
        if (currentKeyBindingButton == null) return;
        buttonBlinkerManager.StopBlinking();
        UpdateKeyText();
    }
}

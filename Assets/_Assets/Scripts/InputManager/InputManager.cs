using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum Binding
{
    Move_Up,
    Move_Down,
    Move_Right,
    Move_Left,
    Interact,
    InteractAlternate,
    GamePause
}
public class InputManager : MonoBehaviour
{
    private const string PLAYER_KEY_BINDING = "PlayerKeyBinding";
    public static InputManager Instance;
    private PlayerInputActions playerInputActions;
    public event EventHandler onInteract;
    public event EventHandler onInteractAlternate;
    public event EventHandler OnGamePause;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        if (PlayerPrefs.HasKey(PLAYER_KEY_BINDING))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_KEY_BINDING));
        }
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.GamePause.performed += GamePause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    private void GamePause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 0f) return;// means if game is paused
        onInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 0f && !GamePauseUI.instance.IsTutorialScreenShowing()) return;// means if game is paused and tutorial screen is closed than dont perform interact
        onInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetVectorMovementNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();

            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();

            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Binding.GamePause:
                return playerInputActions.Player.GamePause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;

            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;

            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;

            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;

            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;

            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;

            case Binding.GamePause:
                inputAction = playerInputActions.Player.GamePause;
                bindingIndex = 0;
                break;
        }

        playerInputActions.Disable();
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            playerInputActions.Enable();
            OnActionRebound();
            PlayerPrefs.SetString(PLAYER_KEY_BINDING, playerInputActions.SaveBindingOverridesAsJson());
        }).Start();
    }
}

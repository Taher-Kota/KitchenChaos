using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnCounterSelectedChanged;
    }

    private void Player_OnCounterSelectedChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        visualGameObject.SetActive(true);
    }

    void Hide()
    {
        visualGameObject.SetActive(false);
    }
}

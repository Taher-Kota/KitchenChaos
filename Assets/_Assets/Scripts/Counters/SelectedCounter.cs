using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private _BaseCounters counters;
    [SerializeField] private GameObject[] visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnCounterSelectedChanged;
    }

    private void Player_OnCounterSelectedChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counters)
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
        foreach (GameObject item in visualGameObject)
        {
            item.SetActive(true);
        }
    }

    void Hide()
    {
        foreach (GameObject item in visualGameObject)
        {
            item.SetActive(false);
        }
    }
}

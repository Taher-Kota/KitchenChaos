using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliverNumberTxt;
    [SerializeField] private GameObject container;
    
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            container.SetActive(true);
            recipeDeliverNumberTxt.text = DeliveryManager.Instance.GetRecipeDeliverdCount().ToString();
        }
    }
}

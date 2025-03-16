using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject recipeTemplate;
    [SerializeField] private TextMeshProUGUI waitingRecipeTxt;

    private void Awake()
    {
        waitingRecipeTxt.gameObject.SetActive(true);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSpawnRecipe += DeliveryManager_OnSpawnRecipe;
        DeliveryManager.Instance.OnCompleteDelivery += DeliverManager_OnCompleteDelivery;
        VisualUpdate();
    }

    private void DeliverManager_OnCompleteDelivery(object sender, EventArgs e)
    {
        VisualUpdate();
    }

    private void DeliveryManager_OnSpawnRecipe(object sender, EventArgs e)
    {
        VisualUpdate();
    }

    void VisualUpdate()
    {
        foreach (Transform child in container.transform)
        {
            if (child == recipeTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach (RecepiesSO recepiesSO in DeliveryManager.Instance.GetRecepiesSOList())
        {
            GameObject tempRecipeTemplate = Instantiate(recipeTemplate, container.transform);
            tempRecipeTemplate.SetActive(true);
            SingleUIDeliveryManager singleUI = tempRecipeTemplate.GetComponent<SingleUIDeliveryManager>();
            singleUI.SetRecipeName(recepiesSO);
            singleUI.SetRecipeName(recepiesSO);

            foreach (KitchenObjectSO kitchenObjectSO in recepiesSO.recipesSO)
            {
                singleUI.SetIconImage(kitchenObjectSO);
            }
        }


    }
}

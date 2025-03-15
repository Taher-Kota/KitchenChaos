using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleUIDeliveryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private Image ingredientImage;

    private void Awake()
    {
        ingredientImage.gameObject.SetActive(false);
    }
    public void SetRecipeName(RecepiesSO recepiesSO)
    {
        recipeName.text = recepiesSO.recipeName;
    }

    public void SetIconImage(KitchenObjectSO kitchenObjectSO)
    {
       Image tempIngredientImage = Instantiate(ingredientImage, iconContainer.transform);
       tempIngredientImage.gameObject.SetActive(true);
       tempIngredientImage.sprite = kitchenObjectSO.sprite;
    }
}

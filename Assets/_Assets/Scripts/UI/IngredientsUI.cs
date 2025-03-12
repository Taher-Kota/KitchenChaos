using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsUI : MonoBehaviour
{
    private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject iconTemplate;
    private void Awake()
    {
        plateKitchenObject = GetComponentInParent<PlateKitchenObject>();
        iconTemplate.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnItemAdd += PlateKitchenObject_OnItemAdd;
    }

    private void PlateKitchenObject_OnItemAdd(object sender, PlateKitchenObject.OnIngredientAddEventArgs e)
    {
       GameObject tempIconTemplate = Instantiate(iconTemplate, transform);
       tempIconTemplate.SetActive(true);
       tempIconTemplate.GetComponent<SingleUIIngredient>().AddIconSprite(e.kitchenObjectSO);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteBurgerVisual : MonoBehaviour
{
    [Serializable]
    public struct BurgerIngredients
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private BurgerIngredients[] burgerIngredients;

    private PlateKitchenObject plateKitchenObject;

    private void Awake()
    {
        plateKitchenObject = GetComponentInParent<PlateKitchenObject>();
        foreach (BurgerIngredients items in burgerIngredients)
        {
            items.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        plateKitchenObject.OnItemAdd += PlateKitchenObject_OnItemAdd;
    }

    private void PlateKitchenObject_OnItemAdd(object sender, PlateKitchenObject.OnIngredientAddEventArgs e)
    {
        foreach (BurgerIngredients items in burgerIngredients)
        {
            if(items.KitchenObjectSO == e.kitchenObjectSO)
            {
                items.gameObject.SetActive(true);
            }
        }
    }
}

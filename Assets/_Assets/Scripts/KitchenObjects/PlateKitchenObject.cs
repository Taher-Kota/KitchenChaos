using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    private List<KitchenObjectSO> kitchenObjectsList = new List<KitchenObjectSO>();
    [SerializeField]  private List<KitchenObjectSO> validKitchenObjects = new List<KitchenObjectSO>();


    public bool TryAddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjects.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectsList.Contains(kitchenObjectSO)) return false;
        kitchenObjectsList.Add(kitchenObjectSO);
        return true;
    }
}
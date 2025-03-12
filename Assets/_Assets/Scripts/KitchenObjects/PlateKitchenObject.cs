using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    public event EventHandler<OnIngredientAddEventArgs> OnItemAdd;
    public class OnIngredientAddEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectsList = new List<KitchenObjectSO>();
    [SerializeField]  private List<KitchenObjectSO> validKitchenObjects = new List<KitchenObjectSO>();


    public bool TryAddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjects.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectsList.Contains(kitchenObjectSO)) return false;
        kitchenObjectsList.Add(kitchenObjectSO);
        OnItemAdd?.Invoke(this,new OnIngredientAddEventArgs { kitchenObjectSO =  kitchenObjectSO });
        return true;
    }
}
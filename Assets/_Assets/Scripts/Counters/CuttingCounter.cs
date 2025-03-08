using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : _BaseCounters
{
    [SerializeField] private KitchenObjectSO slicedKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);
                player.ClearKitchenObjects();
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObjects().SelfDestroy(this);
            KitchenObjects.SpawnKitchenObject(slicedKitchenObjectSO, this);
        }
    }
}

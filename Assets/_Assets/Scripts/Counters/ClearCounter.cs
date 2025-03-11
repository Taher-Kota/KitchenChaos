using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : _BaseCounters
{
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
                ClearKitchenObjects();
            }
            else
            {
                if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObjects().GetKitchenObjectSO()))
                    {
                        GetKitchenObjects().SelfDestroy(this);
                    }
                }
                else
                {
                    if(GetKitchenObjects().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredients(player.GetKitchenObjects().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObjects().SelfDestroy(player);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : _BaseCounters
{
    public static DeliveryCounter Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject platekitchenObject))
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);
                player.ClearKitchenObjects();
                if (DeliveryManager.Instance.CheckOrderComplete(platekitchenObject))
                {
                    GetKitchenObjects().SelfDestroy(this);
                }
                else
                {
                    GetKitchenObjects().SelfDestroy(this);
                }
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
                ClearKitchenObjects();
            }
        }
    }
}

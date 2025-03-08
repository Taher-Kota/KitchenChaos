using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : _BaseCounters
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnContainerInteract;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            OnContainerInteract?.Invoke(this,EventArgs.Empty);
            KitchenObjects.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
}

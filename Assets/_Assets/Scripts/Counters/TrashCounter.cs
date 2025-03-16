using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : _BaseCounters
{
    public static event EventHandler OnObjectTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnObjectTrashed?.Invoke(this,EventArgs.Empty);
            player.GetKitchenObjects().SelfDestroy(player);
        }
    }
}

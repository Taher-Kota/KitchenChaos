using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : _BaseCounters
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjects().SelfDestroy(player);
        }
    }
}

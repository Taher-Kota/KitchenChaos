using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BaseCounters : MonoBehaviour,IKitchenObjectParent
{
    public static event EventHandler OnDropObject; 
    [SerializeField] protected Transform spawnPoint;
    protected KitchenObjects kitchenObjects;

    public virtual void Interact(Player player)
    {
        
    }

    public virtual void InteractAlternate(Player player)
    {
     
    }

    public Transform SetKitchObjectHolderTransform()
    {
        return spawnPoint;
    }

    public void SetNewKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
        if (kitchenObjects != null)
        {
            OnDropObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObjects;
    }

    public void ClearKitchenObjects()
    {
        kitchenObjects = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjects != null;
    }
}

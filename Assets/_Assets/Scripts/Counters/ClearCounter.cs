using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform spawnPoint;
    private KitchenObjects kitchenObjects;

    public void Interact(Player player)
    {
        if (kitchenObjects == null && !player.HasKitchenObject())
        {
            GameObject TempkitchenObject = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            TempkitchenObject.GetComponent<KitchenObjects>().SetKitchenObjectParent(this);
        }
        else if(kitchenObjects == null && player.HasKitchenObject())
        {
            player.GetKitchenObjects().SetKitchenObjectParent(this);
            player.ClearKitchenObjects();
        }
        else
        {
            kitchenObjects.SetKitchenObjectParent(player);
            this.ClearKitchenObjects();
        }
    }

    public Transform SetKitchObjectHolderTransform()
    {
        return spawnPoint;
    }

    public void SetNewKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
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

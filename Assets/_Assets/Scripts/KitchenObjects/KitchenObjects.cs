using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetScriptableObject()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KichenObjectParent)
    {
        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObjects();
        }
        else
        {
            print("net");
            this.KitchenObjectParent = KichenObjectParent;
        }

        if (KichenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Cannot placed two items");
        }
        else
        {
            KichenObjectParent.SetNewKitchenObject(this);
            transform.parent = KichenObjectParent.SetKitchObjectHolderTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }
}

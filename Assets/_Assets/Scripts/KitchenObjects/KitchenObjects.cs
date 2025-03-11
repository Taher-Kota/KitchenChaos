using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KichenObjectParent)
    {
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

    public void SelfDestroy(IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObjectParent.ClearKitchenObjects();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObjects SpawnKitchenObject(KitchenObjectSO slicedKitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        GameObject TempkitchenObject = Instantiate(slicedKitchenObjectSO.prefab);
        KitchenObjects kitchenObjects = TempkitchenObject.GetComponent<KitchenObjects>();
        kitchenObjects.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObjects;
    }
}

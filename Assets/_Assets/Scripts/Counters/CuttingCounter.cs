using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : _BaseCounters
{
    public event EventHandler OnCut;
    public event EventHandler OnNotFullyCut;
    public event EventHandler<OnCuttingProgressClass> OnCuttingProgress;
    public class OnCuttingProgressClass : EventArgs
    {
        public float progressCount;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSO;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (KitchenObjectIsInput(player.GetKitchenObjects().GetKitchenObjectSO()))
                {
                    cuttingProgress = 0;
                    OnCuttingProgress?.Invoke(this, new OnCuttingProgressClass
                    {
                        progressCount = cuttingProgress,
                    });
                    player.GetKitchenObjects().SetKitchenObjectParent(this);
                    player.ClearKitchenObjects();
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                OnNotFullyCut?.Invoke(this,EventArgs.Empty);
                GetKitchenObjects().SetKitchenObjectParent(player);
                this.ClearKitchenObjects();
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
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO kitchenObjectSO = GetKitchenObjects().GetKitchenObjectSO();
            if (!IsKichenObjectAlreadyCut(kitchenObjectSO))
            {
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(kitchenObjectSO);
                cuttingProgress++;
                OnCuttingProgress.Invoke(this, new OnCuttingProgressClass
                {
                    progressCount =  (float)cuttingProgress / cuttingRecipeSO.maxCuttingProgress
                });
                OnCut?.Invoke(this, EventArgs.Empty);
                if (cuttingProgress == cuttingRecipeSO.maxCuttingProgress)
                {
                    GetKitchenObjects().SelfDestroy(this);
                    KitchenObjects.SpawnKitchenObject(GetOutputFromInput(kitchenObjectSO), this);
                }
            }
        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(kitchenObjectSO);
        if (!IsKichenObjectAlreadyCut(kitchenObjectSO))
        {
            if (cuttingRecipeSO != null)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

    private bool IsKichenObjectAlreadyCut(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO item in cuttingRecipeSO)
        {
            if (item.output == kitchenObjectSO) return true;
        }
        return false;
    }

    private bool KitchenObjectIsInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(kitchenObjectSO);
        if (cuttingRecipeSO != null) return true;
        return false;
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO kitchenObjectsSO)
    {
        foreach (CuttingRecipeSO item in cuttingRecipeSO)
        {
            if (item.input == kitchenObjectsSO) return item;
        }
        return null;
    }
}

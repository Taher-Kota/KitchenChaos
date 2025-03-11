using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State 
{
    Idle,
    Fried,
    Frying,
    Burned
}

public class StoveCounter : _BaseCounters
{
    public event EventHandler OnCooking;
    public event EventHandler OnBurned;
    public event EventHandler<ProgressBarUIHandler> StartUI;
    public class ProgressBarUIHandler : EventArgs {
        public float maxTimer;
        public State state = State.Idle;
    }

    [SerializeField] CookedRecipeSO cookedRecipeSO;
    private Coroutine cookingCoroutine;
    private Coroutine burningCoroutine;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (!HasKitchenObject())
            {
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObjects().GetKitchenObjectSO();
                if (cookedRecipeSO.input == kitchenObjectSO)
                {
                    KitchenObjects kitchenObjects = player.GetKitchenObjects();
                    cookingCoroutine = StartCoroutine(StartCooking(cookedRecipeSO.output));
                    kitchenObjects.SetKitchenObjectParent(this);
                    player.ClearKitchenObjects();
                }
            }
            else
            {
                if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObjects().GetKitchenObjectSO()))
                    {
                        StartUI?.Invoke(this, new ProgressBarUIHandler
                        {
                            maxTimer = 0f,
                            state = State.Idle
                        });
                        if (burningCoroutine != null)
                        {
                            StopCoroutine(burningCoroutine);
                            burningCoroutine = null;
                        }
                        OnBurned?.Invoke(this, EventArgs.Empty);
                        GetKitchenObjects().SelfDestroy(this);
                    }
                }
            }
        }
        else
        {
            if (HasKitchenObject())
            {
                if (cookingCoroutine != null)
                {
                    StopCoroutine(cookingCoroutine);
                    cookingCoroutine = null;
                }
                if (burningCoroutine != null)
                {
                    StopCoroutine(burningCoroutine);
                    burningCoroutine = null;
                }
                OnBurned?.Invoke(this, EventArgs.Empty);
                StartUI?.Invoke(this, new ProgressBarUIHandler
                {
                    maxTimer = 0f,
                    state = State.Idle,
                });
                GetKitchenObjects().SetKitchenObjectParent(player);
                ClearKitchenObjects();
            }
        }
    }


    private IEnumerator StartCooking(KitchenObjectSO kitchenObjectSO)
    {
        OnCooking?.Invoke(this, EventArgs.Empty);
        StartUI?.Invoke(this, new ProgressBarUIHandler
        {
            maxTimer = cookedRecipeSO.cookingTimer,
            state = State.Frying
        });

        yield return new WaitForSeconds(cookedRecipeSO.cookingTimer);
        Cook(kitchenObjectSO);
        // Wait for cooking to finish before starting burning
        yield return burningCoroutine = StartCoroutine(StartBurning(cookedRecipeSO.outputBurned));
    }


    private IEnumerator StartBurning(KitchenObjectSO kitchenObjectSO)
    {
        StartUI?.Invoke(this, new ProgressBarUIHandler
        {
            maxTimer = cookedRecipeSO.burnedTimer,
            state = State.Burned
        });
        yield return new WaitForSeconds(cookedRecipeSO.burnedTimer);
        Cook(kitchenObjectSO);
        OnBurned?.Invoke(this, EventArgs.Empty);
    }

    private void Cook(KitchenObjectSO kitchenObjectSO)
    {
        GetKitchenObjects().SelfDestroy(this);
        KitchenObjects.SpawnKitchenObject(kitchenObjectSO, this);
    }
}

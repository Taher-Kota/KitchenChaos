using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnSpawnRecipe;
    public event EventHandler OnCompleteDelivery;
    public event EventHandler OnDeliverFailure;
    public static DeliveryManager Instance;
    [SerializeField] private ReceipeListSO receipeListSO;
    private List<RecepiesSO> waitingRecepieList;
    private float timer = 0f, maxTimer = 4f;
    private int maxOrderCount = 4;
    private int recipeDeliveredCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        waitingRecepieList = new List<RecepiesSO>();
    }


private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer && GameManager.Instance.IsGamePlaying())
        {
            timer = 0f;
            if (waitingRecepieList.Count < maxOrderCount)
            {
                System.Random rng = new System.Random();
                int randomNum = rng.Next(0, receipeListSO.recipiesList.Count);
                RecepiesSO recepiesSO = receipeListSO.recipiesList[randomNum];
                waitingRecepieList.Add(recepiesSO);
                OnSpawnRecipe?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool CheckOrderComplete(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> plateKitchenObjectList =  plateKitchenObject.GetKitchenObjectsList();
        for (int i = 0; i < waitingRecepieList.Count; i++)
        {
            RecepiesSO currentRecepieList = waitingRecepieList[i];
            if(currentRecepieList.recipesSO.Count == plateKitchenObjectList.Count)
            {
                bool matchIngredientFound = true;
                foreach(KitchenObjectSO receipeKitchenObjectSO in currentRecepieList.recipesSO)
                {
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObjectList)
                    {
                        if(receipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        matchIngredientFound = false;
                    }
                }
                if (matchIngredientFound)
                {
                    waitingRecepieList.Remove(currentRecepieList);
                    OnCompleteDelivery?.Invoke(this, EventArgs.Empty);
                    recipeDeliveredCount++;
                    return true;
                }
            }
        }
        OnDeliverFailure?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecepiesSO> GetRecepiesSOList()
    {
        return waitingRecepieList;
    }

    public int GetRecipeDeliverdCount()
    {
        return recipeDeliveredCount;
    }
}

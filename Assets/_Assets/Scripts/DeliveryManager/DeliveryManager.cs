using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnSpawnRecipe;
    public event EventHandler OnCompleteDelivery;
    public static DeliveryManager Instance;
    [SerializeField] private ReceipeListSO receipeListSO;
    private List<RecepiesSO> waitingRecepieList;
    private float timer = 0f, maxTimer = 4f;
    private int maxOrderCount = 4;

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
        if (timer > maxTimer)
        {
            timer = 0f;
            if (waitingRecepieList.Count < maxOrderCount)
            {
                RecepiesSO recepiesSO = receipeListSO.recipiesList[UnityEngine.Random.Range(0, receipeListSO.recipiesList.Count)];
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
                    return true;
                }
            }
        }
        return false;
    }

    public List<RecepiesSO> GetRecepiesSOList()
    {
        return waitingRecepieList;
    }
}

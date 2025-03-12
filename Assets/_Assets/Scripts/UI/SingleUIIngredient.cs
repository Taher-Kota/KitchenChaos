using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleUIIngredient : MonoBehaviour
{
    [SerializeField] private Image iconeSprite;

    public void AddIconSprite(KitchenObjectSO kitchenObjectSO)
    {
        iconeSprite.sprite = kitchenObjectSO.sprite;
    }
}

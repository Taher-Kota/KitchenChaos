using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CookedRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public KitchenObjectSO outputBurned;
    public float cookingTimer;
    public float burnedTimer;
}

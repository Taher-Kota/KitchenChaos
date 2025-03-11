using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : _BaseCounters
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private float spawnTime = 0f, maxSpawnTime = 4f;
    private int spawnAmount = 0, maxSpawnAmount = 4;

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > maxSpawnTime)
        {
            spawnTime = 0f;
            if (spawnAmount < maxSpawnAmount)
            {
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                spawnAmount++;
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (spawnAmount > 0)
            {
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                spawnAmount--;
                KitchenObjects.SpawnKitchenObject(kitchenObjectSO, player);
                ClearKitchenObjects();
            }
        }
    }
}

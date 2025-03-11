using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterCounterVisual : MonoBehaviour
{
    private List<GameObject> plateVisualList;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private GameObject spawnPoint;
    private PlateCounter plateCounter;
    private void Awake()
    {
        plateCounter = gameObject.GetComponent<PlateCounter>();
    }
    private void Start()
    {
        plateVisualList = new List<GameObject>();
        plateCounter.OnPlateSpawn += PlateCounter_OnPlateSpawn;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject tempPlate = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(tempPlate);
        Destroy(tempPlate);
    }

    private void PlateCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        GameObject tempPlate = Instantiate(plateVisual, spawnPoint.transform);
        tempPlate.transform.localPosition = new Vector3(0f, .1f * plateVisualList.Count, 0f);
        plateVisualList.Add(tempPlate);
    }
}

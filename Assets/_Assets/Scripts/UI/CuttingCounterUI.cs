using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private GameObject parent;

    private void Start()
    {
        cuttingCounter.OnCuttingProgress += CuttingCounter_OnCuttingProgress;
    }
    private void CuttingCounter_OnCuttingProgress(object sender, CuttingCounter.OnCuttingProgressClass e)
    {
        progressBar.fillAmount = e.progressCount;
        Show();
        if(e.progressCount == 1)
        {
            Hide();
        }
    }

    private void Show()
    {
        parent.SetActive(true);
    }

    private void Hide()
    {
        parent.SetActive(false);
    }
}

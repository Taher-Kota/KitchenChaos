using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        StoveCounter.Instance.OnCooking += StoveCounter_OnCooking;
        StoveCounter.Instance.OnCooked += StoveCounter_OnCooked;
    }

    private void StoveCounter_OnCooked(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }

    private void StoveCounter_OnCooking(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    const string STOVE_COOKING = "StoveCooking";
    [SerializeField] private GameObject stoveBurningVisual;
    [SerializeField] private GameObject fireParticleSystem;
    private StoveCounter stoveCounter;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        stoveCounter = GetComponent<StoveCounter>();
    }
    private void Start()
    {
        stoveCounter.OnCooking += StoveCounter_OnCooking;
        stoveCounter.OnBurned += StoveCounter_OnBurned;
    }

    private void StoveCounter_OnCooking(object sender, System.EventArgs e)
    {
        Show();
    }
    private void StoveCounter_OnBurned(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        anim.SetBool(STOVE_COOKING, true);
        stoveBurningVisual.SetActive(true);
        fireParticleSystem.SetActive(true);
    }

    private void Hide()
    {
        anim.SetBool(STOVE_COOKING, false);
        stoveBurningVisual.SetActive(false);
        fireParticleSystem.SetActive(false);
    }
}

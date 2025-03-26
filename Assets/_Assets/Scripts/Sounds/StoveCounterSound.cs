using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private StoveCounter stoveCounter;

    private void Awake()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
    }

    private void Start()
    {
        stoveCounter.OnCooking += StoveCounter_OnCooking;
        stoveCounter.OnBurned += StoveCounter_OnBurned;
    }

    private void StoveCounter_OnBurned(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }

    private void StoveCounter_OnCooking(object sender, System.EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
        audioSource.Play();
    }
}

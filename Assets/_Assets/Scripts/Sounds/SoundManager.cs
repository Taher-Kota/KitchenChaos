using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string SOUND_EFFECT_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set;}
    [SerializeField] SoundClipSO SoundClipSO;
    private float defaultVolume = .5f;

    private void Awake()
    {
        Instance = this;
        defaultVolume = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME,defaultVolume);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverFailure += DeliveryManger_OnDeliverFailure;
        DeliveryManager.Instance.OnCompleteDelivery += DeliverManager_OnCompleteDelivery;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
        Player.Instance.OnGrabObject += Player_OnGrabObject;
        _BaseCounters.OnDropObject += _BaseCounters_OnDropObject;
        CuttingCounter.OnCutting += CuttingCounter_OnCutting;
    }
    private void OnDestroy()
    {
        _BaseCounters.OnDropObject -= _BaseCounters_OnDropObject;
        CuttingCounter.OnCutting -= CuttingCounter_OnCutting;
        TrashCounter.OnObjectTrashed -= TrashCounter_OnObjectTrashed;
    }
    private void CuttingCounter_OnCutting(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(SoundClipSO.chop[Random.Range(0, SoundClipSO.chop.Length)],
            cuttingCounter.transform.position);
    }

    private void _BaseCounters_OnDropObject(object sender, System.EventArgs e)
    {
        _BaseCounters baseCounter = sender as _BaseCounters;
        PlaySound(SoundClipSO.objectDrop[Random.Range(0, SoundClipSO.objectDrop.Length)],
            baseCounter.transform.position);
    }

    private void Player_OnGrabObject(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.objectPickup[Random.Range(0, SoundClipSO.objectPickup.Length)],
            Player.Instance.transform.position);
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(SoundClipSO.trash[Random.Range(0, SoundClipSO.trash.Length)],
            trashCounter.transform.position);
    }

    private void DeliverManager_OnCompleteDelivery(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.deliverySuccess[Random.Range(0, SoundClipSO.deliverySuccess.Length)], 
            DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManger_OnDeliverFailure(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.deliveryFail[Random.Range(0, SoundClipSO.deliveryFail.Length)],
            DeliveryCounter.Instance.transform.position);
    }

    public void NumberPopUpSound()
    {
        PlaySound(SoundClipSO.warning[0],Vector3.zero);
    }
    
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(SoundClipSO.warning[1],position);
    }

    public void PlaySound(AudioClip audioClip, Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(audioClip, audioPos, defaultVolume);
    }

    public void ChangeVolume(float volume)
    {
        defaultVolume = volume;
        PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME,defaultVolume);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME) * 100f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundClipSO SoundClipSO;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverFailure += DeliveryManger_OnDeliverFailure;
        DeliveryManager.Instance.OnCompleteDelivery += DeliverManager_OnCompleteDelivery;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
        Player.Instance.OnGrabObject += Player_OnGrabObject;
        _BaseCounters.OnDropObject += _BaseCounters_OnDropObject;
        CuttingCounter.OnCutting += CuttingCounter_OnCutting;
    }

    private void CuttingCounter_OnCutting(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(SoundClipSO.chop[Random.Range(0, SoundClipSO.chop.Length)],
            cuttingCounter.transform.position, 1f);
    }

    private void _BaseCounters_OnDropObject(object sender, System.EventArgs e)
    {
        _BaseCounters baseCounter = sender as _BaseCounters;
        PlaySound(SoundClipSO.objectDrop[Random.Range(0, SoundClipSO.objectDrop.Length)],
            baseCounter.transform.position, 1f);
    }

    private void Player_OnGrabObject(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.objectPickup[Random.Range(0, SoundClipSO.objectPickup.Length)],
            Player.Instance.transform.position, 1f);
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(SoundClipSO.trash[Random.Range(0, SoundClipSO.trash.Length)],
            trashCounter.transform.position, 1f);
    }

    private void DeliverManager_OnCompleteDelivery(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.deliverySuccess[Random.Range(0, SoundClipSO.deliverySuccess.Length)], 
            DeliveryCounter.Instance.transform.position, 1f);
    }

    private void DeliveryManger_OnDeliverFailure(object sender, System.EventArgs e)
    {
        PlaySound(SoundClipSO.deliveryFail[Random.Range(0, SoundClipSO.deliveryFail.Length)],
            DeliveryCounter.Instance.transform.position, 1f);
    }

    public void PlaySound(AudioClip audioClip, Vector3 audioPos,float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, audioPos, volume);
    }

}

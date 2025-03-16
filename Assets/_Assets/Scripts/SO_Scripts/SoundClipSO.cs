using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundClipSO : ScriptableObject
{
    public AudioClip[] footsteps;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] drop;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] chop;
    public AudioClip panSizzling;
    public AudioClip[] trash;
    public AudioClip[] warning;
}

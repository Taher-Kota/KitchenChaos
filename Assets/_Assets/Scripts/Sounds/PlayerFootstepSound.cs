using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    [SerializeField] SoundClipSO soundClipSO;
    private float timer;
    [SerializeField] private float maxTimer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (Player.Instance.IsWalking())
        {
            if (timer > maxTimer)
            {
                timer = 0f;
                AudioSource.PlayClipAtPoint(soundClipSO.footsteps[Random.Range(0, soundClipSO.footsteps.Length)],
                    transform.position, 1f);
            }
        }
    }
}

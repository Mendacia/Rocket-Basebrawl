using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectHolder : MonoBehaviour
{
    [SerializeField] private List<AudioClip> soundEffects = null;
    [SerializeField] private AudioSource sfxPlayer = null;
    private AudioClip sfxClip;

    public void SilverSoundEffect()
    {
        sfxClip = soundEffects[0];
        sfxPlayer.clip = sfxClip;
        sfxPlayer.Play();
    }

    public void GoldSoundEffect()
    {
        sfxClip = soundEffects[1];
        sfxPlayer.clip = sfxClip;
        sfxPlayer.Play();
    }
}

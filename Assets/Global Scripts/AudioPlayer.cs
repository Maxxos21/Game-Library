using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : PersistentSingleton<AudioPlayer>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    public void PlayAudio(int id)
    {
        audioSource.PlayOneShot(audioClip[id]);
    }
    
    public void PlayAudio(int id, float vol)
    {
        audioSource.PlayOneShot(audioClip[id], vol);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}

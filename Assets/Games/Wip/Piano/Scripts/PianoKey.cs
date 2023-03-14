using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PianoKey : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    private PianoController pianoController;

    private void Awake()
    {
        pianoController = FindObjectOfType<PianoController>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (pianoController != null)
        {
            pianoController.AddToSequence(this);
        }
    }
}

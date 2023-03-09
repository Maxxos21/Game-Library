using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    private AudioSource audioPlayer;

    private PianoController piano;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    void Start()
    {
        piano = FindObjectOfType<PianoController>();
    }

    public void OnClick()
    {
        audioPlayer.Play();

        piano.TestKey(this);
    }
}

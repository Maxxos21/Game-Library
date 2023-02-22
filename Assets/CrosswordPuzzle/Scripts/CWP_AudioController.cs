using UnityEngine;

public class CWP_AudioController : MonoBehaviour
{
    private AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    public void LoadAudioClip(AudioClip clip)
    {
        player.clip = clip;
    }

    public void PlaySound()
    {
        player.Play();
    }
}

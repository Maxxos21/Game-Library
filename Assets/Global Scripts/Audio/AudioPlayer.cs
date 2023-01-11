using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioPlayer : PersistentSingleton<AudioPlayer>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    public void PlayAudio(int id)
    {
        audioSource.PlayOneShot(audioClip[id]);

        // Audio setup
        audioSource.volume = 0.35f;
        audioSource.pitch = Random.Range(0.5f, 1.1f);
    }
    
    public void PlayAudio(int id, float vol)
    {
        audioSource.PlayOneShot(audioClip[id], vol);
    }
}

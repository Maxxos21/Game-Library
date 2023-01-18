using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioPlayer : PersistentSingleton<AudioPlayer>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    public void PlayAudio(int id)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.PlayOneShot(audioClip[id]);
        audioSource.pitch = Random.Range(0.5f, 1.1f);
    }
    
    public void PlayAudio(int id, float vol)
    {
        audioSource.PlayOneShot(audioClip[id], vol);
    }

}

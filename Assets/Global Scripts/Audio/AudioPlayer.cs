using UnityEngine.SceneManagement;
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

    // press esc to return to main menu
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}

using UnityEngine;

public class WTQ_SoundController : MonoBehaviour
{
    [SerializeField] AudioClip soundCorrect;
    [SerializeField] AudioClip soundIncorrect;

    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void PlayCorrectSound()
    {
        audioPlayer.clip = soundCorrect;
        audioPlayer.Play();
    }

    public void PlayIncorrectSound()
    {
        audioPlayer.clip = soundIncorrect;
        audioPlayer.Play();
    }
}

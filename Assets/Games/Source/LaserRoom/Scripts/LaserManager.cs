using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LaserManager : MonoBehaviour
{
    private MainLaser[] lasers;
    private bool allLasersHitPreviousFrame;
    private float timeAllLasersHit;

    void Awake()
    {
        lasers = FindObjectsOfType<MainLaser>();
    }
    
    void Update()
    {
        if (CheckLasers())
        {
            if (!allLasersHitPreviousFrame)
            {
                allLasersHitPreviousFrame = true;
                timeAllLasersHit = Time.time;

                    if (AudioPlayer.Instance != null)
                    {
                        AudioPlayer.Instance.PlayAudio(0);
                    }
            }
            else
            {
                if (Time.time - timeAllLasersHit >= 1.2f)
                {
                    if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
            }
        }
        else
        {
            allLasersHitPreviousFrame = false;
        }
    }
    public bool CheckLasers()
    {
        foreach (MainLaser laser in lasers)
        {
            if (!laser.isHittingReceiver)
            {
                return false;
            }
        }
        return true;
    }
}
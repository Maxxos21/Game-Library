using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private MainLaser[] lasers;
    [SerializeField] private bool allLasersHit = false;
    [SerializeField] private GameObject nextLevel;
    private bool allLasersHitPreviousFrame;
    private float timeAllLasersHit;

    void Start()
    {
        nextLevel.SetActive(false);
    }

    void Update()
    {
        if (CheckLasers())
        {
            if (!allLasersHitPreviousFrame)
            {
                allLasersHitPreviousFrame = true;
                timeAllLasersHit = Time.time;
            }
            else
            {
                if (Time.time - timeAllLasersHit >= 2)
                {
                    Debug.Log("All lasers are hitting the receiver for 2 seconds!");
                    StartCoroutine(LoadNextLevel());
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

    IEnumerator LoadNextLevel()
    {
        allLasersHit = true;
        yield return new WaitForSeconds(2);
        nextLevel.SetActive(true);
    }
}
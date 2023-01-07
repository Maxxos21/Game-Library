using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private MainLaser[] lasers;
    [SerializeField] private bool allLasersHit = false;
    [SerializeField] private GameObject nextLevel;

    void Start()
    {
        nextLevel.SetActive(false);
    }

    void Update()
    {
        if (CheckLasers())
        {
            Debug.Log("All lasers are hitting the receiver!");
            StartCoroutine(LoadNextLevel());
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
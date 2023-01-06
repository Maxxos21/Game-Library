using UnityEngine;

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
            nextLevel.SetActive(true);
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
        allLasersHit = true;
        return true;
    }
}
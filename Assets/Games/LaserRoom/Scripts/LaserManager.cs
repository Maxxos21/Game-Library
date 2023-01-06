using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private MainLaser[] laser;

   void Update()
    {
        if (laser[0].isHittingTarget && laser[1].isHittingTarget && laser[2].isHittingTarget)
        {
            Debug.Log("All lasers are hitting their targets!");
        }
    }
}
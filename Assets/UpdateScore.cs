using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;

    void Start()
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + RadialBar.score;
        }
    }
}

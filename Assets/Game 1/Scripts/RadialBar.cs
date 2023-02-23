using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{

    [SerializeField] private Image dial;

    public int currentValue, maxValue;


    public void Add(int value)
    {
        currentValue += value;

        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            dial.transform.Rotate(0, 0, -36);
        }
    }

    public void Subtract(int value)
    {
        currentValue -= value;
        if (currentValue < 0)
        {
            currentValue = 0;
        }
        else
        {
            dial.transform.Rotate(0, 0, 36);
        }
    }
}

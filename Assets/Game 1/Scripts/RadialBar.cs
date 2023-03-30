using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{

    [SerializeField] private Image dial;
    public int currentValue, maxValue;
    public static int score = 0;


    IEnumerator BounceDial()
    {
        dial.transform.Rotate(0, 0, -15);
        yield return new WaitForSeconds(0.2f);
        dial.transform.Rotate(0, 0, 15);
    }


    public void Add(int value)
    {
        currentValue += value;

        if (currentValue > maxValue)
        {
            currentValue = maxValue;

            // Rotate dial -15 and bounce back to normal
            StartCoroutine(BounceDial());
        }
        else
        {
            dial.transform.Rotate(0, 0, -36);
        }

        score += 1;
    }

    public void Subtract(int value)
    {
        currentValue -= value;

        if (currentValue <= 0)
        {
            currentValue = 0;
        }
        else
        {
            dial.transform.Rotate(0, 0, 36);

            if (score > 0)
            {
                score -= 1;
            }
        }
    }
}

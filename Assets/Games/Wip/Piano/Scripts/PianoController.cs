using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoController : MonoBehaviour
{
    [SerializeField] private PianoKey[] sequence;

    private int currentSequenceIndex;

    public void TestKey(PianoKey keyPlayed)
    {
        if (keyPlayed == sequence[currentSequenceIndex])
        {
            // DEBUG CURR
            Debug.Log("Correct");

            currentSequenceIndex++;
            if (currentSequenceIndex > sequence.Length - 1)
            {
                Debug.Log("Game end");
            }
        }
        else
        {
            Debug.Log("Incorrect");
            currentSequenceIndex = 0;
        }
    }
}

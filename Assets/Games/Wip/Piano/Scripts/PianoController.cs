using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Lean;

namespace Lean.Transition.Method
{
    public class PianoController : MonoBehaviour
    {
        [SerializeField] private List<PianoKey> correctSequence = new List<PianoKey>();
        private List<PianoKey> playerSequence = new List<PianoKey>();
        [SerializeField] private LeanRectTransformAnchoredPosition_y boxPosition;



        public void AddToSequence(PianoKey currentKey)
        {
            // Add the key from event to the player sequence
            playerSequence.Add(currentKey);

            // Check if the player played an incorrect key
            for (int i = 0; i < playerSequence.Count; i++)
            {
                if (playerSequence[i] != correctSequence[i])
                {
                    // Clear the player sequence and return
                    playerSequence.Clear();
                    return;
                }
            }

            // If the player sequence is the same length as the correct sequence, check if it's correct
            if (playerSequence.Count == correctSequence.Count)
            {
                bool isCorrect = true; // set to true, assume correct until proven wrong
                for (int i = 0; i < correctSequence.Count; i++)
                {
                    if (playerSequence[i].name != correctSequence[i].name)
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {
                    boxPosition.Register();
                }
                else
                {
                    Debug.Log("Lost");
                }

                playerSequence.Clear();
            }
        }
    }
}
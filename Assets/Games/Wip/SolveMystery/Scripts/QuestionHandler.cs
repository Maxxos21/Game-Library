using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Transition;
using UnityEngine.SceneManagement;

public class QuestionHandler : MonoBehaviour
{
    [SerializeField] private Button correctAnswer;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private static int score;
    [SerializeField] private GameObject winPanel, losePanel;


    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => CheckAnswer(j));
        }
    }

    public void CheckAnswer(int j)
    {
        // Disable current question
        this.gameObject.SetActive(false);
        
        // Score Increase
        if (correctAnswer == buttons[j])
        {
            score++;
        }

        // Show Result
        if (questionPanel != null)
        {
            questionPanel.SetActive(true);
        }
        else
        {
            if (score == 3)
            {
                winPanel.SetActive(true);
            }
            else
            {
                losePanel.SetActive(true);
            }
        }
    }

    public void ResetGame()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

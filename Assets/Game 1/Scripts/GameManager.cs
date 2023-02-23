using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Answers[] answers;

    // Variable
    private int score;
    public TMP_Text scoreText;


    // Reference
    public RadialBar radialBar;


    void Awake()
    {
        radialBar = FindObjectOfType<RadialBar>();
    }

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = answers[i].answer;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => CheckAnswer(j));
        }
    }

    void CheckAnswer(int index)
    {
        if (answers[index].isCorrect)
        {
            buttons[index].GetComponent<Image>().color = Color.green;
            
            
            radialBar.Add(20);
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            buttons[index].GetComponent<Image>().color = Color.red;


            radialBar.Subtract(20);
            score--;
            scoreText.text = "Score: " + score.ToString();
        }

        buttons[index].interactable = false;
    }
}

[System.Serializable]
public class Answers
{
    public string answer;
    public bool isCorrect;
}


using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Answers[] answers;

    // Variables
    [SerializeField] TMP_Text scoreDisplay;

    // Reference
    public RadialBar radialBar;


    void Awake()
    {
        radialBar = FindObjectOfType<RadialBar>();
    }

    void Start()
    {
        scoreDisplay.text = "Score: " + RadialBar.score;

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
            scoreDisplay.text = "Score: " + RadialBar.score;
        }
        else
        {
            buttons[index].GetComponent<Image>().color = Color.red;

            radialBar.Subtract(20);
            scoreDisplay.text = "Score: " + RadialBar.score;
        }

        buttons[index].interactable = false;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

[System.Serializable]
public class Answers
{
    public string answer;
    public bool isCorrect;
}


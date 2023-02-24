using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] Button[] buttons;
    [SerializeField] Answers[] answers;

    // Variables
    [HideInInspector]
    [SerializeField] TMP_Text scoreDisplay;
    [HideInInspector]
    [SerializeField] TMP_Text questionText;

    // Menus
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject introMenu;
    [SerializeField] GameObject gameMenu;

    public string question;

    // Reference
    public RadialBar radialBar;


    void Awake()
    {
        radialBar = FindObjectOfType<RadialBar>();
    }

    void Start()
    {
        scoreDisplay.text = "Score: " + RadialBar.score;
        questionText.text = question;

        for (int i = 0; i < buttons.Length; i++)
        {
            Answers temp = answers[i];
            int randomIndex = Random.Range(i, answers.Length);
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
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

    public void SwitchMenu(GameObject menu)
    {
        startMenu.SetActive(false);
        introMenu.SetActive(false);
        gameMenu.SetActive(false);

        menu.SetActive(true);
    }
}

[System.Serializable]
public class Answers
{
    public string answer;
    public bool isCorrect;
}


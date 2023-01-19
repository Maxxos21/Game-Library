using UnityEngine.EventSystems;
using System;
using UnityEngine;
using TMPro;

public class WorldMapManager : MonoBehaviour
{
    // Questions
    public CountryQuestion[] CorrectCountries;
    public int currentQuestion = 0;

    // Texts
    [HideInInspector]
    public TMP_Text questionText;
    [HideInInspector]
    public TMP_Text responseText;

    // Colors
    [HideInInspector]
    public string[] colors;

    // Marker to spawn
    [HideInInspector]
    public GameObject markerCorrect;
    [HideInInspector]
    public GameObject markerWrong;

    void Start()
    {
        SetCurrentQuestion(currentQuestion);
    }

    public void SetCurrentQuestion(int currentQuestion)
    {
        Debug.Log("Current Question: " + currentQuestion);
        Debug.Log("Correct Countries Length: " + CorrectCountries.Length);

        if (currentQuestion < CorrectCountries.Length)
        {
            questionText.text = CorrectCountries[currentQuestion].question;
        }
        else
        {
            Debug.Log("You Win!");
            questionText.text = "You Win!";
        }
    }

    public void ShowHint()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            responseText.text = (CorrectCountries[currentQuestion].hint);
        }

    }

}

[Serializable]
public class CountryQuestion
{
    public GameObject country;
    public string question;
    public string hint;
}
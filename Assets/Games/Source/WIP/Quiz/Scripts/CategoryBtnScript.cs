using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoryBtnScript : MonoBehaviour
{
    [SerializeField] private TMP_Text categoryTitleText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button btn;

    public Button Btn { get => btn; }

    public void SetButton(string title, int totalQuestion)
    {
        categoryTitleText.text = title;
        levelText.text = totalQuestion.ToString();
    }

}

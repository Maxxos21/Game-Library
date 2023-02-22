using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WTQ_GameController : MonoBehaviour
{
    [Header("Point Values")]
    [SerializeField] private int pointValue;
    [SerializeField] private int deductValue;

    [Space(15)]
    [SerializeField] private Quote[] questions;

    [Space(15)]
    [SerializeField] private float nextQuestionDelay;

    [Space(15)]
    [SerializeField] private UnityEvent endGameActions;

    [Header("Components")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TMP_Text speechText;
    [SerializeField] private TMP_Text creditText;
    [SerializeField] private TMP_Text questionTracker;
    [SerializeField] private GameObject wordbankBackground;
    [SerializeField] private Button hintButton;

    private string[] words;
    [HideInInspector()] public int currentOrderIndex = 0;
    private int questionIndex = 0;
    private int hintsAvailable = 0;
    private float nextQuestionTimer= 0.0f;
    private bool nextQuestionQueued = false;
    
    private WTQ_SoundController audioPlayer;

    private GameObject[] buttons;
    private Vector2 buttonSpaceSize;

    private Quote quote;

    [Serializable] private struct Quote
    {
        public string question;
        public string credit;
        [Tooltip("The number of hints permitted for this question")]
        public int hints;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<WTQ_SoundController>();

        buttonSpaceSize = wordbankBackground.GetComponent<RectTransform>().sizeDelta;
        buttonSpaceSize.x /= -2.2f;
        buttonSpaceSize.y /= 2;

        SetUpQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        HandleNextQuestionQueue();
    }

    private void SetUpQuestion()
    {
        if (questionIndex < questions.Length)
        {
            // clear speech text
            words = null;
            speechText.SetText("");

            // reset orderIndex
            currentOrderIndex = 0;

            quote = questions[questionIndex];

            // set available hints
            hintsAvailable = quote.hints;
            hintButton.interactable = true;

            string[] quoteWords = quote.question.Split(' ');

            buttons = new GameObject[quoteWords.Length];

            Vector3 pos = new Vector3(buttonSpaceSize.x + 5, buttonSpaceSize.y, 0);

            bool createSingle = false;
            if (quoteWords.Length % 2 != 0)
            {
                createSingle = true;
            }

            string[] reconstructedWords = null; 
            quoteWords = ReconstructWordArray(createSingle, quoteWords, reconstructedWords);
            
            int[] correctOrder = new int[quoteWords.Length];
            for (int i = 0; i < correctOrder.Length; i++)
            {
                correctOrder[i] = i;
            }

            PlaceRandomWord(quoteWords, correctOrder, pos);

            // set quote person
            creditText.SetText(quote.credit);

            questionIndex++;

            // set tracker text
            string trackerText = questionIndex + "/" + questions.Length;
            questionTracker.SetText(trackerText);
        }
        else
        {
            Debug.Log("Game end");

            endGameActions.Invoke();
        }
    }

    private void PlaceRandomWord(string[] words, int[] correctOrder, Vector3 pos)
    {
        int i = UnityEngine.Random.Range(0, words.Length);
        string word = words[i];

        GameObject button = Instantiate(buttonPrefab, wordbankBackground.transform);
        buttons = AddToArray(buttons, button);

        GameObject textObject = button.transform.GetChild(0).gameObject;
        var text = textObject.GetComponent<TMP_Text>();
        text.SetText(word);

        var size = text.GetPreferredValues();

        size.x += 20;
        size.y = 25 + 15;

        button.GetComponent<RectTransform>().sizeDelta = size;

        Vector2 additive = new Vector2((size.x / 2) + 5, (size.y) + 20);
        pos.x += additive.x;

        if ((pos.x + additive.x) >= wordbankBackground.GetComponent<RectTransform>().sizeDelta.x / 2 || pos.y == buttonSpaceSize.y)
        {
            pos.x = buttonSpaceSize.x + additive.x;
            pos.y -= additive.y;
        }

        button.transform.localPosition = new Vector3(pos.x, pos.y, 0);

        pos.x += additive.x;

        // assign orderIndex
        text.GetComponent<WTQ_WordButton>().orderIndex = correctOrder[i];

        // remove word from array
        words = RemoveIndexFromArray(words, i);
        correctOrder = RemoveIndexFromArray(correctOrder, i);

        if (words != null) {
            PlaceRandomWord(words, correctOrder, pos);
        }
    }

    private string[] ReconstructWordArray(bool createSingle, string[] oldArr, string[] reconstructedArr)
    {
        int length = 1;
        if (reconstructedArr != null)
        {
            length += reconstructedArr.Length;
        }

        string[] newArr = new string[length];

        if (reconstructedArr != null)
        {
            for (int i = 0; i < reconstructedArr.Length; i++) {
                newArr[i] = reconstructedArr[i];
            }
        }

        string word = oldArr[0];
        oldArr = RemoveFirstInArray(oldArr);

        if (oldArr != null)
        {
            if (createSingle)
            {
                int chance = UnityEngine.Random.Range(0, 101);
                if (chance > 50)
                {
                    createSingle = false;
                }
                else
                {
                    word += " " + oldArr[0];
                    oldArr = RemoveFirstInArray(oldArr);
                }
            }
            else
            {
                word += " " + oldArr[0];
                oldArr = RemoveFirstInArray(oldArr);
            }
        }

        newArr[length - 1] = word;

        if (oldArr == null)
        {
            return newArr;
        }
        else
        {
            return ReconstructWordArray(createSingle, oldArr, newArr);
        }
    }

    public int AddWord(string word)
    {
        words = AddToArray(words, word);
        currentOrderIndex++;

        DisplayText();

        return words.Length - 1;
    }

    private void DisplayText()
    {
        var str = "";

        if (words != null)
        {
            for (int i = 0; i < words.Length; i++)
            {
                str += words[i];

                if (i < words.Length - 1)
                {
                    str += " ";
                }
            }
        }

        speechText.SetText(str);

        CheckAnswer(str);
    }

    private void CheckAnswer(string answer)
    {
        if (quote.question.Equals(answer))
        {
            // TODO: Add points here

            hintButton.interactable = false;
            
            audioPlayer.PlayCorrectSound();

            QueueNextQuestion();
        }
    }

    public void DeductPoints()
    {
        // TODO: Remove points here

        audioPlayer.PlayIncorrectSound();
    }

    private string[] AddToArray(string[] oldArr, string toAdd)
    {
        int length = 1;

        if (oldArr != null)
        {
            length += oldArr.Length;
        }

        string[] newArr = new string[length];

        if (oldArr != null)
        {
            for (int i = 0; i < oldArr.Length; i++)
            {
                newArr[i] = oldArr[i];
            }
        }

        newArr[length - 1] = toAdd;

        return newArr;
    }

    private string[] RemoveIndexFromArray(string[] oldArr, int index)
    {
        string[] newArr = null;

        int length = oldArr.Length - 1;
        if (length > 0)
        {
            newArr = new string[length];

            int newIndex = 0;
            for (int i = 0; i < oldArr.Length; i++)
            {
                if (i != index)
                {
                    newArr[newIndex] = oldArr[i];

                    newIndex++;
                }
            }
        }

        return newArr;
    }

    private int[] RemoveIndexFromArray(int[] oldArr, int index) {
        int[] newArr = null;

        int length = oldArr.Length - 1;
        if (length > 0)
        {
            newArr = new int[length];

            int newIndex = 0;
            for (int i = 0; i < oldArr.Length; i++)
            {
                if (i != index)
                {
                    newArr[newIndex] = oldArr[i];

                    newIndex++;
                }
            }
        }

        return newArr;
    }

    private GameObject[] AddToArray(GameObject[] oldArr, GameObject toAdd)
    {
        int length = 1;

        if (oldArr != null)
        {
            length += oldArr.Length;
        }

        GameObject[] newArr = new GameObject[length];

        if (oldArr != null)
        {
            for (int i = 0; i < oldArr.Length; i++)
            {
                newArr[i] = oldArr[i];
            }
        }

        newArr[length - 1] = toAdd;

        return newArr;
    }

    private string[] RemoveFirstInArray(string[] oldArr)
    {
        string[] newArr = null;

        if (oldArr.Length != 1)
        {
            int length = oldArr.Length - 1;
            newArr = new string[length];

            for (int i = 0; i < length; i++) {
                newArr[i] = oldArr[i + 1];
            }
        }

        return newArr;
    }

    private void QueueNextQuestion()
    {
        nextQuestionQueued = true;
    }

    private void HandleNextQuestionQueue()
    {
        if (nextQuestionQueued)
        {
            nextQuestionTimer += Time.deltaTime;

            if (nextQuestionTimer >= nextQuestionDelay)
            {
                nextQuestionQueued = false;
                nextQuestionTimer = 0.0f;

                // destroy old buttons
                for (int i = 0; i < buttons.Length; i++)
                {
                    Destroy(buttons[i]);
                }

                SetUpQuestion();
            }
        }
    }

    public void HandleHintButton()
    {
        if (hintsAvailable > 0)
        {
            hintsAvailable--;
            if (hintsAvailable <= 0)
            {
                hintButton.interactable = false;
            }

            // TODO: Remove points here

            WTQ_WordButton[] wordButtons = FindObjectsOfType<WTQ_WordButton>();

            // find next word set
            for (int i = 0; i < wordButtons.Length; i++)
            {
                if (wordButtons[i].orderIndex == currentOrderIndex)
                {
                    wordButtons[i].OnClick();
                    break;
                }
            }

            audioPlayer.PlayIncorrectSound();
        }
    }
}

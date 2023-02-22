using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CWP_CrosswordSpace : MonoBehaviour
{
    // COLORS
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color correctColor;

    private Color defaultColor;

    // DISPLAY
    [SerializeField] private TMP_Text indexDisplay;
    [SerializeField] private TMP_Text letterDisplay;

    // GAME CHECKS
    [HideInInspector] public char letterCorrect;
    [HideInInspector] public char letterText;
    [HideInInspector] public int[] wordIndex;

    // POSITION
    [HideInInspector] public int row;
    [HideInInspector] public int column;
    
    // GAME FLAGS
    [HideInInspector] public bool isActive = true;
    [HideInInspector] public bool isSelected = false;
    [HideInInspector] public bool waitingForKeyUp = false;

    // COMPONENTS
    private Image img;

    // MISC
    private WordBatch[] batches;
    private CWP_CrosswordSpace[] otherSpaces;
    
    // CONTROLLERS
    private CWP_GameController controller;
    private CWP_AudioController audioController;

    public struct WordBatch {
        public int wordIndex;
        public CWP_CrosswordSpace[] batch;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<CWP_GameController>();
        audioController = FindObjectOfType<CWP_AudioController>();

        img = gameObject.GetComponent<Image>();
        defaultColor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyPress();
    }

    public void OnClick()
    {
        if (isActive)
        {
            if (isSelected)
            {
                SetUnselected();
            }
            else
            {
                UnselectAllSpaces();

                SetSelected();
            }
        }
    }

    public void SetDisplayIndex(int index)
    {
        indexDisplay.SetText(index.ToString());
    }

    public void UnselectAllSpaces()
    {
        if (otherSpaces == null || otherSpaces.Length > 0)
        {
            otherSpaces = FindObjectsOfType<CWP_CrosswordSpace>();
        }

        foreach (CWP_CrosswordSpace space in otherSpaces)
        {
            if (space.isActive)
            {
                space.SetUnselected();
            }                    
        }
    }

    public void SetSelected()
    {
        isSelected = true;        
        img.color = selectedColor;
    }

    public void SetUnselected()
    {
        isSelected = false;
        img.color = defaultColor;
    }

    public void SetCorrect()
    {
        isActive = false;
        img.color = correctColor;
    }

    private void HandleKeyPress()
    {
        if (isSelected && isActive && !waitingForKeyUp)
        {
            foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                   if (otherSpaces == null || otherSpaces.Length > 0)
                    {
                        otherSpaces = FindObjectsOfType<CWP_CrosswordSpace>();
                    }

                    foreach (CWP_CrosswordSpace space in otherSpaces)
                    {
                        if (space.isActive)
                        {
                            space.waitingForKeyUp = true;
                        }                    
                    }

                    if (IsValidKeyCode(vKey))
                    {
                        letterText = (char)vKey;
                        letterDisplay.SetText(letterText.ToString());

                        CheckAnswers();

                        audioController.PlaySound();
                    }
                    else if (vKey == KeyCode.Backspace)
                    {
                        letterDisplay.SetText("");
                    }
                }
            }
        }
        else
        {
            foreach(KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyUp(vkey))
                {
                    waitingForKeyUp = false;
                }
            }
        }
    }

    private void CheckAnswers()
    {
        bool attemptGoNext = true;

        foreach (int index in wordIndex)
        {
            int numCorrect = 0;
            CWP_CrosswordSpace[] batch = GetWordBatch(index);
            foreach (CWP_CrosswordSpace space in batch)
            {
                if (space.letterCorrect == space.letterText)
                {
                    numCorrect++;
                }
            }

            if (numCorrect >= batch.Length)
            {
                attemptGoNext = false;
                controller.IncrementWordsCorrect();

                foreach (CWP_CrosswordSpace space in batch)
                {
                    space.SetCorrect();
                }
            }
        }

        if (attemptGoNext)
        {
            controller.AttemptGoToNextSpace(row, column);
        }
    }

    private bool IsValidKeyCode(KeyCode k)
    {
        // check to see if this is a single character string, validating it as a letter
        string str = k.ToString();
        char[] chars = str.ToCharArray();

        return chars != null && chars.Length == 1;
    }

    private CWP_CrosswordSpace[] GetWordBatch(int index)
    {
        // check if wordBatch exists
        CWP_CrosswordSpace[] batch = null;
        if (batches != null && batches.Length > 0)
        {
            for (int i = 0; i < batches.Length; i++)
            {
                if (batches[i].wordIndex == index)
                {
                    batch = batches[i].batch;
                    break;
                }
            }
        }

        // if there is no batch, build it
        if (batch == null || batch.Length <= 0)
        {
            foreach (CWP_CrosswordSpace space in otherSpaces)
            {
                foreach (int wordIndex in space.wordIndex)
                {
                    if (wordIndex == index)
                    {
                        batch = Push(batch, space);
                        break;
                    }
                }
            }

            WordBatch batchData = new WordBatch
            {
                wordIndex = index,
                batch = batch
            };

            batches = Push(batches, batchData);
        }

        return batch;
    }

    private CWP_CrosswordSpace[] Push(CWP_CrosswordSpace[] arr, CWP_CrosswordSpace value)
    {
        int length = 1;
        if (arr != null && arr.Length > 0)
        {
            length += arr.Length;
        }

        CWP_CrosswordSpace[] newArr = new CWP_CrosswordSpace[length];
        newArr[0] = value;
        
        if (arr != null && arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i + 1] = arr[i];
            }
        }

        return newArr;
    }

    private WordBatch[] Push(WordBatch[] arr, WordBatch value)
    {
        int length = 1;
        if (arr != null && arr.Length > 0)
        {
            length += arr.Length;
        }

        WordBatch[] newArr = new WordBatch[length];
        newArr[0] = value;

        if (arr != null && arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i + 1] = arr[i];
            }
        }

        return newArr;
    }
}

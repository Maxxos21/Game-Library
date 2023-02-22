using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CWP_GameController : MonoBehaviour
{
    [SerializeField] private PromptData[] promptData;

    [Space(15)]
    [SerializeField] private GameObject spacePrefab;

    [Header("Components")]
    [SerializeField] private TMP_Text acrossDisplay;
    [SerializeField] private TMP_Text downDisplay;

    [SerializeField] private TMP_Text timeDisplay;

    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float gameEndDelay;

    [Header("Audio")]
    [SerializeField] private AudioClip keyPressSound;
    [SerializeField] private AudioClip winGameSound;


    private CWP_AudioController audioController;

    private int wordsComplete = 0;

    [HideInInspector] public bool timeActive = true;

    private float time = 0;
    private GameLayout layout;
    private int numOverrideIndices = 0;
    private bool lastAcross = true;

    private const int offsetX = 0;
    private const int offsetY = 1;
    private const int spaceSize = 26;
    private const int numRows = 25;
    private const int numColumns = 24;

    public struct GameLayout
    {
        public Rows[] rows;
    }

    public struct Rows
    {
        public GameObject[] columns;
    }

    [Serializable] public struct PromptData
    {
        public string prompt;
        [Tooltip("If a row and columnn start in the same space, use this to override the second entry")]
        public int indexOverride;
        public string answer;
        public bool across;
        [Tooltip("Starting space on a 24x25 grid.")]
        public Vector2 startCoord;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioController = FindObjectOfType<CWP_AudioController>();
        audioController.LoadAudioClip(keyPressSound);

        SetUpBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive)
        {
            time += Time.deltaTime;
            timeDisplay.SetText(TimeSpan.FromSeconds(time).ToString(@"mm\:ss"));
        }
    }


    private void SetUpBoard()
    {
        // initialize layout
        layout = new GameLayout {};
        layout.rows = new Rows[numRows];

        for (int i = 0; i < layout.rows.Length; i++)
        {
            layout.rows[i].columns = new GameObject[numColumns];
        }

        for (int i = 0; i < promptData.Length; i++)
        {
            //  place boardspaces
            PromptData prompt = promptData[i];
            string answer = prompt.answer.ToLower();
            char[] chars = answer.ToCharArray();

            float startCoordX = prompt.startCoord.x - 1;
            float startCoordY = prompt.startCoord.y - 1;

            for (int n = 0; n < chars.Length; n++)
            {
                int indexX = (int)startCoordY;
                int indexY = (int)startCoordX;

                if (prompt.across)
                {
                    indexY += n;
                }
                else
                {
                    indexX += n;
                }

                if (layout.rows[indexY].columns[indexX] == null)
                {
                    GameObject o = Instantiate(spacePrefab, gameObject.transform);
                    o.transform.localPosition= new Vector3((indexY * spaceSize) + offsetX, (-indexX * spaceSize) - offsetY, 0);

                    layout.rows[indexY].columns[indexX] = o;
                }
                
                CWP_CrosswordSpace space = layout.rows[indexY].columns[indexX].GetComponent<CWP_CrosswordSpace>();
                space.wordIndex = Push(space.wordIndex, i);
                space.letterCorrect = chars[n];

                space.column = indexX;
                space.row = indexY;

                if (n == 0)
                {
                    int displayIndex = i + 1 - numOverrideIndices;
                    if (prompt.indexOverride != 0)
                    {
                        displayIndex = prompt.indexOverride;
                        numOverrideIndices++;
                    }

                    space.GetComponent<CWP_CrosswordSpace>().SetDisplayIndex(displayIndex);

                    // add prompt to display
                    TMP_Text display = downDisplay;
                    if (prompt.across)
                    {
                        display = acrossDisplay;
                    }

                    string text = display.text;
                    text += "\n\n" + displayIndex + ") " + prompt.prompt;
                    display.SetText(text);
                }
            }
        }
    }

    public void IncrementWordsCorrect()
    {
        wordsComplete++;

        if (wordsComplete >= promptData.Length)
        {
            timeActive = false;
            
            string str = "Nice Job!\nYou completed the puzzle in:\n<b>" + TimeSpan.FromSeconds(time).ToString(@"mm\:ss") + "</b>";
            gameEndPanel.GetComponentInChildren<TMP_Text>().SetText(str);

            StartCoroutine(WaitThenScaleIn(gameEndPanel, new Vector3(1, 1, 1), scaleDuration, gameEndDelay));
        }
    }

    public void AttemptGoToNextSpace(int row, int column)
    {
        GameObject[] potentialSpaces = null;

        int nextRow = row + 1;
        int nextColumn = column + 1;
        
        bool isAcross = false;

        if (nextRow < layout.rows.Length)
        {
            GameObject nextSpace = layout.rows[nextRow].columns[column];
            if (nextSpace != null)
            {
                isAcross  = true;
                potentialSpaces = Push(potentialSpaces, nextSpace);
            }
        }

        if (nextColumn < layout.rows[row].columns.Length)
        {
            GameObject nextSpace = layout.rows[row].columns[nextColumn];
            if (nextSpace != null)
            {
                potentialSpaces = Push(potentialSpaces, nextSpace);
            }
        }

        if (potentialSpaces != null)
        {
            CWP_CrosswordSpace space = null;
            if (potentialSpaces.Length == 1)
            {
                lastAcross = isAcross;
                space = potentialSpaces[0].GetComponent<CWP_CrosswordSpace>();
            }
            else if (potentialSpaces.Length == 2)
            {
                if (!lastAcross)
                {
                    space = potentialSpaces[0].GetComponent<CWP_CrosswordSpace>();
                }
                else
                {
                    space = potentialSpaces[1].GetComponent<CWP_CrosswordSpace>();
                }
            }

            if (space.isActive)
            {
                space.UnselectAllSpaces();
                space.SetSelected();
            }
        }
    }

    IEnumerator AnimateScale(GameObject o, Vector3 scale, float duration)
    {
        float time = 0.0f;
        Vector3 scaleStart = o.transform.localScale;
        while (time <= duration)
        {
            float t = time / duration;
            t = t * t * (3.0f - 2.0f * t);

            o.transform.localScale = Vector3.Lerp(scaleStart, scale, t);

            time += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitThenScaleIn(GameObject o, Vector3 scale, float scaleDuration, float delay)
    {
        yield return new WaitForSeconds(delay);

        audioController.LoadAudioClip(winGameSound);
        audioController.PlaySound();

        StartCoroutine(AnimateScale(o, scale, scaleDuration));
    }

    public int[] Push(int[] arr, int value)
    {
        int length = 1;
        if (arr != null && arr.Length > 0)
        {
            length += arr.Length;
        }

        int[] newArr = new int[length];
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

    public GameObject[] Push(GameObject[] arr, GameObject value)
    {
        int length = 1;
        if (arr != null && arr.Length > 0)
        {
            length += arr.Length;
        }

        GameObject[] newArr = new GameObject[length];
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

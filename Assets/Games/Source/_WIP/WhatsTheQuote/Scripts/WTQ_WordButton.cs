using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WTQ_WordButton : MonoBehaviour
{
    [TextArea]
    [Tooltip("This note does not do anything. It is for information purposes only.")]
    public string note = "Colors can be be edited from within the WTQ_WordButton script.";

    private static Color c_unselected = new Vector4(1.0f, 0.8f, 0.8f, 1.0f);
    private static Color c_selected = new Vector4(0.863f, 0.153f, 0.141f, 1.0f);

    private string word;
    private WTQ_GameController gameManager;
    [HideInInspector()] public int wordIndex;
    [HideInInspector()] public int orderIndex;

    private bool selected = false;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<WTQ_GameController>();
        word = gameObject.GetComponent<TMP_Text>().text;
        img = gameObject.GetComponentInParent<Image>();

        img.color = c_unselected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!selected)
        {
            if (orderIndex == gameManager.currentOrderIndex)
            {
                SetSelected();
                wordIndex = gameManager.AddWord(word);
            }
            else
            {
                gameManager.DeductPoints();
            }
        }
    }

    private void SetSelected()
    {
        selected = true;
        img.color = c_selected;
    }
}

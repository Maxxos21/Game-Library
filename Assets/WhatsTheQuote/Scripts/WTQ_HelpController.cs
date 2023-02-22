using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WTQ_HelpController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;

    [Header("Pip Components")]
    [SerializeField] GameObject pipPrefab;
    [SerializeField] private Canvas pipDock;
    [SerializeField] private Sprite pipFilled;
    [SerializeField] private Sprite pipEmpty;
    [Tooltip("This much match the width of your pip Prefab")]
    [SerializeField] private float pipWidth;
    [SerializeField] private float pipSpacing;

    [Header("Components")]
    [SerializeField] GameObject helpWindow;
    [SerializeField] private GameObject[] pageButtons;

    private Image[] pips;

    private int pageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreatePips();

        HideAllPages();
        ShowPage(pageIndex);
    }

    public void HandleHelpButton()
    {
        if (gameObject.activeSelf)
        {
            HideHelpWindow();
        }
        else
        {
            ShowHelpWindow();
        }
    }

    public void ShowHelpWindow()
    {
        gameObject.SetActive(true);
    }

    public void HideHelpWindow()
    {
        gameObject.SetActive(false);
    }

    private void CreatePips()
    {
        if (pages != null)
        {
            if (pages.Length > 1)
            { // only create pips if there is more than one page

                // calculate space and start position
                float totalSpace = (pages.Length * pipWidth) + ((pages.Length - 1) * pipSpacing);
                float startX = (totalSpace / -2) + (pipWidth / 2);

                pips = new Image[pages.Length];

                Vector3 pos = new Vector3(startX, 0, 0);

                for (int i = 0; i < pages.Length; i++)
                {
                    // create pips, add to array
                    GameObject pip = Instantiate(pipPrefab, pipDock.transform);
                    pips[i] = pip.GetComponent<Image>();

                    // position pip
                    pip.transform.localPosition = pos;

                    // adjust position for next pip
                    pos.x += pipWidth + pipSpacing;
                }
            }
            else
            {
                for (int i = 0; i < pageButtons.Length; i++)
                { // if 1 page, deactivate next page buttons
                    pageButtons[i].SetActive(false);
                }
            }
        }
    }

    private void HidePage(int index)
    {
        pages[index].SetActive(false);

        if (pips != null)
        {
            pips[index].sprite = pipEmpty;
        }
    }

    private void HideAllPages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            HidePage(i);
        }
    }

    private void ShowPage(int index)
    {
        pages[index].SetActive(true);

        if (pips != null)
        {
            pips[index].sprite = pipFilled;
        }
    }

    public void NextPage()
    {
        HidePage(pageIndex);

        pageIndex = Cycle(pageIndex, 0, pages.Length - 1, 1);
        ShowPage(pageIndex);
    }

    public void PreviousPage()
    {
        HidePage(pageIndex);

        pageIndex = Cycle(pageIndex, 0, pages.Length - 1, -1);
        ShowPage(pageIndex);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private int Cycle (int current, int min, int max, int increment)
    {
        current += increment;
        if (current > max)
        {
            current = min;
        }
        else if (current < min)
        {
            current = max;
        }

        return current;
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DynamicScrollItem : MonoBehaviour
{
    public void OpenBundleGame()
    {
        var sceneName = gameObject.GetComponentInChildren<TMP_Text>().text;
        SceneManager.LoadScene(sceneName);
    }
}

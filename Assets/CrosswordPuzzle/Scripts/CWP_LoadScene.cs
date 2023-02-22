using UnityEngine;
using UnityEngine.SceneManagement;

public class CWP_LoadScene : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

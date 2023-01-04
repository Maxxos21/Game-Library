using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DynamicScrollView : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;

    private List<Color> colors = new List<Color> { Color.red, Color.blue, Color.green, Color.yellow, Color.cyan, Color.magenta };

    private void Start()
    {
        foreach (string sceneName in GetSceneNames())
        {
            GameObject button = Instantiate(prefab, scrollViewContent);
            button.GetComponentInChildren<TMP_Text>().text = sceneName;
        }
    }

    private static List<string> GetSceneNames()
    {
        List<string> sceneNames = new List<string>();
        string[] scenePaths = UnityEditor.AssetDatabase.FindAssets("t:Scene", new string[] { "Assets/Games/_Scene" });

        foreach (string scenePath in scenePaths)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEditor.AssetDatabase.GUIDToAssetPath(scenePath));
            sceneNames.Add(sceneName);
        }
        
        return sceneNames;
    }
}

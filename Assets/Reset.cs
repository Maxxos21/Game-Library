using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : PersistentSingleton<Reset>
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Load Menu Scene
            SceneManager.LoadScene("Menu");
            Debug.Log("Reset");
        }
    }
}

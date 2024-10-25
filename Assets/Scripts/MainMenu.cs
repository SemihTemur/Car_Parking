using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        MemoryManagement();
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    // Memory management
    public void MemoryManagement()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 0);
            PlayerPrefs.SetInt("Level", 1);
        }
    }

}

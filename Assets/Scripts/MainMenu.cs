using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



// This script controls the main menu functionality (loading levels, quitting the game)
public class MainMenu : MonoBehaviour
{

    public GameObject titleScreen;
    public GameObject levelSelectScreen;

    // This method loads the first level (scene index 1 in Build Settings)
    public void Level_01()
    {
        // Load the scene asynchronously by its build index (1 means the second scene in Build Settings)
        SceneManager.LoadSceneAsync(1);
    }

    // This method quits the game when called
    public void QuitGame()
    {
        // Quits the application when built (this won’t stop play mode in the Unity editor)
        Application.Quit();
    }

    private void OnEnable()
    {
        // Initialize the GameManager's references to the title and level select screens
        string currentScreen = string.IsNullOrEmpty(GameManager.instance.displayScreenName) ? "Title_Screen" : GameManager.instance.displayScreenName;
        if (currentScreen == "Title_Screen")
            titleScreen.SetActive(true);
        else
            levelSelectScreen.SetActive(true);
    }
}

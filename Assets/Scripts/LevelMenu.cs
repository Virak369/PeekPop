using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script manages the level selection menu
public class LevelMenu : MonoBehaviour
{
    // Array of level buttons (assign these in the Unity Inspector)
    public Button[] buttons;

    private void Awake()
    {
        // Default to 1 if not set
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");

        // Initialize level buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevels; i++)
        {
            buttons[i].interactable = true;
        }
        buttons[0].interactable = true;
    }

    // Function to load the selected level
    // Called when the player clicks a level button
    public void OpenLevel(int levelId)
    {
        // The scene name must match exactly (e.g., "Level 1", "Level 2", etc.)
        string levelName = "Level " + levelId;

        //fix display level
        GameManager.instance.currentLevel = levelId;

        // Load the selected level scene
        SceneManager.LoadScene(levelName);
    }
}

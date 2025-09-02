using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("LevelSelection Settings")]
    [Tooltip("Level buttons gameobject")]
    [SerializeField] GameObject[] levelButtons;// Array of level buttons in the UI

    [Tooltip("Text that contain total star that already collected by user (no need to change in inspector)")]
    [SerializeField] Text totalCollectedStarText;// UI text to display total stars collected

    [Tooltip("Gameobject panel that will be showed when all level clear (no need to change in inspector)")]
    [SerializeField] GameObject gameClearPanel;// UI panel that shows up when all levels are cleared

    [Tooltip("Maximum stars that can be get on one level")]
    const int starsPerLevel = 3;// Each level gives a maximum of 3 stars

    [Header("LevelSelection UI Settings")]
    [Tooltip("Sprite of button that already get perfect stars")]
    [SerializeField] Sprite panelCompleted;// Sprite for a fully completed level (all stars collected)

    [Tooltip("Sprite of background button that already get stars but still not perfect")]
    [SerializeField] Sprite panelActive;// Sprite for a level that is unlocked but not completed perfectly

    [Tooltip("Sprite of background button that still not get any stars")]
    [SerializeField] Sprite panelNotActive;// Sprite for a locked level

    [Tooltip("Sprite of active stars")]
    [SerializeField] Sprite starActive;// Sprite for a collected star

    [Tooltip("Sprite of not active stars")]
    [SerializeField] Sprite starNotActive;// Sprite for a missing/uncollected star

    void Start()
    {
        // Initialize level data when the game starts
        InitiateLevelData();
    }

    void InitiateLevelData()
    {
        // If no level is unlocked yet, unlock the first level by default
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
        }

        // Get the currently unlocked level index from PlayerPrefs
        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");

        // Variable to store the sum of all collected stars
        int tempTotalCollectedStars = 0;

        // Loop through each level button to set up its state
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Set the level number text on the button
            levelButtons[i].transform.Find("LevelText").GetComponent<Text>().text = (i + 1).ToString();

            // Assign the level ID to the LevelButton component
            levelButtons[i].GetComponent<LevelButton>().id = i + 1;

            // Load how many stars were collected on this level
            int tempStar = PlayerPrefs.GetInt("Level" + (i + 1));

            // Add collected stars to total stars
            tempTotalCollectedStars += tempStar;

            // Store the total stars in the LevelButton component
            levelButtons[i].GetComponent<LevelButton>().totalStar = tempStar;

            // Change button background depending on status
            if (i < currentUnlockedLevel)
            {
                // If level is unlocked
                if (tempStar == 0)
                    levelButtons[i].GetComponent<Image>().sprite = panelActive; // Unlocked but not completed
                else
                    levelButtons[i].GetComponent<Image>().sprite = panelCompleted; // Completed perfectly
            }
            else
            {
                // Level is locked
                levelButtons[i].GetComponent<Image>().sprite = panelNotActive;
            }

            // Set stars display (active or inactive) under the level button
            for (int j = 0; j < starsPerLevel; j++)
            {
                // Check if the "Star" UI exists in the button
                if (levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)) != null)
                {
                    if (j < tempStar)

                        // Star collected → show active star
                        levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)).GetComponent<Image>().sprite = starActive;
                    else

                        // Star not collected → show inactive star
                        levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)).GetComponent<Image>().sprite = starNotActive;
                }
            }
        }

        // Show total collected stars in the UI
        totalCollectedStarText.text = tempTotalCollectedStars.ToString();

        // Check if all levels are cleared perfectly
        if (tempTotalCollectedStars >= levelButtons.Length * starsPerLevel)
            gameClearPanel.SetActive(true);
        else
            gameClearPanel.SetActive(false);

        // Save the maximum number of levels available
        PlayerPrefs.SetInt("MaxLevel", levelButtons.Length);
    }
}

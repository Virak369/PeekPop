using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private const string KEY_UNLOCKED = "UnlockedLevels";
    private const string KEY_MAXLEVEL = "MaxLevel";

    [Header("LevelManager Settings")]
    [Tooltip("Selected level id (no need to change in inspector)")]
    public int selectedLevel;

    [Tooltip("Gameobject of the star image that will be showed in the end of game (no need to change in inspector)")]
    [SerializeField] Image[] starObjects;

    [Tooltip("Total star that get on current selected level (no need to change in inspector)")]
    [SerializeField] int totalStar;

    [Tooltip("Minimum stars needed to count as a win/unlock next level")]
    [SerializeField] int minStarsToUnlock = 1;

    [Tooltip("Level text that showed on main game scene (no need to change in inspector)")]
    public Text levelText;

    [Tooltip("Level text that showed on win panel (no need to change in inspector)")]
    public Text levelWinPanelText;

    [Tooltip("Level text that showed on lose panel (no need to change in inspector)")]
    public Text levelLosePanelText;

    [Tooltip("Button next level that will be showed in the end of game, if you win (no need to change in inspector)")]
    [SerializeField] GameObject nextLevelButton;

    [Tooltip("Based name of scene, must be same with that set on LevelButton")]
    public string levelSceneString;

    [Header("LevelManager UI")]
    [Tooltip("Sprite of active stars")]
    [SerializeField] Sprite starActive;

    [Tooltip("Sprite of not active stars")]
    [SerializeField] Sprite starNotActive;

    [Header("Max Level Settings")]
    [Tooltip("How many levels exist in the game (must be set in inspector!)")]
    public int totalLevels = 25;

    private void Awake()
    {

        if (!PlayerPrefs.HasKey(KEY_MAXLEVEL))
            PlayerPrefs.SetInt(KEY_MAXLEVEL, totalLevels);

        instance = this;
    }

    private void Start()
    {
        InitiateLevel();
    }

    void InitiateLevel()
    {
        if (!PlayerPrefs.HasKey(KEY_UNLOCKED))
            PlayerPrefs.SetInt(KEY_UNLOCKED, 1);

        // Use the actual current playing level, not the highest unlocked
        selectedLevel = GameManager.instance.currentLevel;

        // Hide next button if this is the last level
        if (PlayerPrefs.GetInt(KEY_MAXLEVEL, selectedLevel) == selectedLevel)
            nextLevelButton.SetActive(false);

        // Display current level everywhere
        levelText.text = "Level " + selectedLevel;
        levelWinPanelText.text = selectedLevel.ToString();
        levelLosePanelText.text = selectedLevel.ToString();
    }

    public void SetStars(int totalStar)
    {
        this.totalStar = totalStar;

        // Update star UI
        for (int i = 0; i < starObjects.Length; i++)
            starObjects[i].sprite = (i < totalStar) ? starActive : starNotActive;

        // Save best stars for this level
        string levelKey = $"Level{selectedLevel}";
        int prevBest = PlayerPrefs.GetInt(levelKey, 0);
        if (this.totalStar > prevBest)
            PlayerPrefs.SetInt(levelKey, this.totalStar);

        // ---- Only unlock next level on WIN ----
        bool won = this.totalStar >= minStarsToUnlock;
        int currentUnlocked = PlayerPrefs.GetInt(KEY_UNLOCKED, 1);
        int maxLevel = PlayerPrefs.GetInt(KEY_MAXLEVEL, totalLevels);

        if (won && selectedLevel >= currentUnlocked && selectedLevel < maxLevel)
        {
            PlayerPrefs.SetInt(KEY_UNLOCKED, selectedLevel + 1);
        }

        PlayerPrefs.Save();
    }

    public void OnNextLevelButtonClicked()
    {
        // Do NOT change unlock state here; it's handled in SetStars when you win
        int maxLevel = PlayerPrefs.GetInt(KEY_MAXLEVEL, selectedLevel);
        int nextId = Mathf.Min(selectedLevel + 1, maxLevel);

        // Update GameManager so the new scene knows which level is active
        GameManager.instance.currentLevel = nextId;
        JumpToOtherScene.quickGoToScene("Level " + nextId);
    }
}

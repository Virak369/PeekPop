using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string displayScreenName;
    public int currentLevel = 1;

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists (Singleton pattern)
        if (instance == null)
        {
            instance = this;

            // Don't destroy this AudioManager when loading new scenes
            DontDestroyOnLoad(gameObject);

            // Clear all PlayerPrefs on first Awake (if desired)
            //PlayerPrefs.DeleteAll();
        }
        else
        {
            // If another AudioManager exists, destroy it (avoid duplicates)
            Destroy(gameObject);
        }
    }
}

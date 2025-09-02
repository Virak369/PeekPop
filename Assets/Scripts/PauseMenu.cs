using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the Pause Menu in the game
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Stop the game time
    }
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        GameManager.instance.displayScreenName = "Level_Select_Screen";
        Time.timeScale = 1; // Resume game time when going back to main menu
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume game time when going back to main menu
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; // Resume game time when going back to main menu
    }
}

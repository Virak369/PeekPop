using UnityEngine;

// This script manages and plays sound effects (SFX) in your game
public class SFXManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip jumpSound;
    public AudioClip clickSound;

    // Method to play the jump sound
    public void PlayJump()
    {
        // Plays the jumpSound once, without interrupting other sounds
        sfxSource.PlayOneShot(jumpSound);
    }

    // Method to play the click sound
    public void PlayClick()
    {
        // Plays the clickSound once, without interrupting other sounds
        sfxSource.PlayOneShot(clickSound);
    }
}

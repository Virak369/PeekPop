using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This attribute makes the class visible in the Unity Inspector,
// so you can create and edit Sound objects directly in the editor.
[System.Serializable]
public class Sound
{
    // The name of the sound (useful for identifying or playing by name in scripts)
    public string name;

    // The actual audio file (AudioClip) that will be played in Unity
    public AudioClip clip;
}

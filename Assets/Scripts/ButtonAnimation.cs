using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    // Reference to the Button component
    Button btn;

    // Scale to enlarge the button to when clicked
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1);

    private void Awake()
    {
        // Get the Button component attached to this GameObject
        btn = gameObject.GetComponent<Button>();

        // Add the Anim method as a listener to the button's onClick event
        btn.onClick.AddListener(Anim);
    }

    // This method runs when the button is clicked
    void Anim()
    {
        // First, scale the button up to 1.2x size over 0.1 seconds
        LeanTween.scale(gameObject, upScale, 0.1f);

        // Then, scale it back down to original size (1x) over 0.1 seconds, after a delay of 0.1 seconds
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }
}

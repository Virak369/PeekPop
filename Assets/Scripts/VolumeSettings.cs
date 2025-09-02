using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sound Settings")]
    public Slider soundSlider;
    public Button soundPlusBtn;
    public Button soundMinusBtn;

    [Header("SFX Settings")]
    public Slider sfxSlider;
    public Button sfxPlusBtn;
    public Button sfxMinusBtn;

    private void Start()
    {
        // Load saved preferences (optional)
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        AudioManager.instance.musicSource.volume = soundSlider.value;
        AudioManager.instance.sfxSource.volume = sfxSlider.value;

        // Add listeners
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        soundPlusBtn.onClick.AddListener(() => AdjustSlider(soundSlider, 0.1f));
        soundMinusBtn.onClick.AddListener(() => AdjustSlider(soundSlider, -0.1f));
        sfxPlusBtn.onClick.AddListener(() => AdjustSlider(sfxSlider, 0.1f));
        sfxMinusBtn.onClick.AddListener(() => AdjustSlider(sfxSlider, -0.1f));
    }

    void AdjustSlider(Slider slider, float delta)
    {
        slider.value = Mathf.Clamp(slider.value + delta, slider.minValue, slider.maxValue);
    }

    void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundVolume", volume);
        // Apply to AudioMixer or AudioSource
    }

    void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // Apply to AudioMixer or AudioSource
    }
}

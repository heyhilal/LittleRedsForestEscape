using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.Instance;

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;

        ApplySettings();

        musicSlider.onValueChanged.AddListener(delegate { ApplySettings(); });
        sfxSlider.onValueChanged.AddListener(delegate { ApplySettings(); });
        muteToggle.onValueChanged.AddListener(delegate { ApplySettings(); });
    }

    public void ApplySettings()
    {
        if (audioManager == null)
            audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found!");
            return;
        }

        bool muted = muteToggle.isOn;

        audioManager.SetMusicVolume(muted ? 0f : musicSlider.value);
        audioManager.SetSFXVolume(muted ? 0f : sfxSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
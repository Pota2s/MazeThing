using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioInitializer : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider uiSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] AudioMixer audioMixer;
    void OnEnable()
    {
        AudioService.Initialize(audioMixer);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume",1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1.0f);
        uiSlider.value = PlayerPrefs.GetFloat("UiVolume", 1.0f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        masterSlider.onValueChanged.AddListener(AudioService.SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(AudioService.SetSfxVolume);
        uiSlider.onValueChanged.AddListener(AudioService.SetUiVolume);
        musicSlider.onValueChanged.AddListener(AudioService.SetMusicVolume);

    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(AudioService.SetMasterVolume);
        sfxSlider.onValueChanged.RemoveListener(AudioService.SetSfxVolume);
        uiSlider.onValueChanged.RemoveListener(AudioService.SetUiVolume);
        musicSlider.onValueChanged.RemoveListener(AudioService.SetMusicVolume);
    }
}

using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioService
{
	private static AudioMixer audioMixer;

	public static float MasterVolume { get; private set; }
    public static float SfxVolume {get; private set; }
    public static float UiVolume { get; private set; }
    public static float MusicVolume { get; private set; }

	public static float LinearToDB(float value) {
		if (value <= 0) return -80;
        return Mathf.Log10(value) * 20f;
    }

	private static void DuckVolume()
	{
        audioMixer.SetFloat("MusicDuckingVolume", -15);
    }

	private static void UnduckVolume()
	{
		audioMixer.SetFloat("MusicDuckingVolume", 0);
	}

    public static void Initialize(AudioMixer audioMixer)
	{
		AudioService.audioMixer = audioMixer;
		MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
		SfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1.0f);
		UiVolume = PlayerPrefs.GetFloat("UiVolume", 1.0f);
		MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        audioMixer.SetFloat("MasterVolume", LinearToDB(MasterVolume));
        audioMixer.SetFloat("SfxVolume", LinearToDB(SfxVolume));
        audioMixer.SetFloat("UiVolume", LinearToDB(UiVolume));
        audioMixer.SetFloat("MusicVolume", LinearToDB(MusicVolume));

		TimeService.OnPause += DuckVolume;
		TimeService.OnResume += UnduckVolume;
    }

	public static void SetMasterVolume(float volume)
	{
		if (audioMixer == null) return;

		MasterVolume = volume;
		audioMixer.SetFloat("MasterVolume", MasterVolume);
		PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
	}

	public static void SetSfxVolume(float value)
	{
        if (audioMixer == null) return;

        SfxVolume = value;
        audioMixer.SetFloat("SfxVolume", SfxVolume);
        PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
	}

	public static void SetUiVolume(float value)
	{
        if (audioMixer == null) return;

        UiVolume = value;
		audioMixer.SetFloat("UiVolume", UiVolume);
		PlayerPrefs.SetFloat("UiVolume", UiVolume);
	}

	public static void SetMusicVolume(float value)
	{
        if (audioMixer == null) return;

        MusicVolume = value;
        audioMixer.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

}

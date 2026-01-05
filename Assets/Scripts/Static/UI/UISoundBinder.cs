using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UISoundBinder : MonoBehaviour
{
	private List<Button> buttons;
	void Awake()
	{
		buttons = GetComponentsInChildren<Button>(true).ToList();

		if (UiSoundManager.Instance != null)
		{
			OnSoundManagerInitialized(UiSoundManager.Instance);
			return;
		}
		UiSoundManager.OnInitialized += OnSoundManagerInitialized;
	}

	void OnSoundManagerInitialized(UiSoundManager soundManager)
	{
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(soundManager.PlayUISoundClick);
        }
    }
}

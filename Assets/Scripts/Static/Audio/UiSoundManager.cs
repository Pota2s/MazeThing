using UnityEngine;
using System.Collections.Generic;
using System;

public class UiSoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clickSounds;
    public static event Action<UiSoundManager> OnInitialized;
    private AudioSource audioSource;
    public static UiSoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        OnInitialized.Invoke(this);
    }

    public void PlayUISoundClick()
    {
        AudioClip clip = clickSounds[UnityEngine.Random.Range(0,clickSounds.Count)];
        audioSource.PlayOneShot(clip);
    }

}

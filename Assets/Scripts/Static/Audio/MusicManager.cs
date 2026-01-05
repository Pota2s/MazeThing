using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public event Action OnFinished;


    [SerializeField] List<AudioClip> musicClips;
    
    private AudioSource audioSource;
    private Coroutine coroutine;
    public static MusicManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        audioSource = GetComponent<AudioSource>();
        coroutine = StartCoroutine(DetectFinish());
    }

    void PlayRandomMusic()
    {
        audioSource.clip = musicClips[UnityEngine.Random.Range(0, musicClips.Count)];
        audioSource.Play();
    }

    IEnumerator DetectFinish()
    {
        while (true)
        {
            PlayRandomMusic();
            yield return new WaitUntil(() => !audioSource.isPlaying);
            OnFinished?.Invoke();
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

}

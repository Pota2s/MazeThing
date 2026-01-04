using System;
using UnityEngine;

public class TimeService
{
    private static bool isPaused = false;
    private static float timeScale = 1f;

    public static event Action OnPause;
    public static event Action OnResume;
    public static event Action<float> OnTimeScaleChanged;

    public static void Pause()
    {
        SilentPause();
        OnPause?.Invoke();
    }

    public static void SilentPause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public static void Resume()
    {
        SilentResume();
        OnResume?.Invoke();
    }

    public static void SilentResume()
    {
        isPaused = false;
        Time.timeScale = timeScale;
    }

    public static void SetTimeScale(float newTimeScale)
    {
        timeScale = Math.Max(float.Epsilon, newTimeScale);
        if (!isPaused)
        {
            Time.timeScale = timeScale;
        }
        OnTimeScaleChanged.Invoke(timeScale);
    }

    public static void Toggle() 
    {
        if (isPaused) { 
            Resume();
            return;
        }

        Pause();
    }

}

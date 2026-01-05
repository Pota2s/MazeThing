using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static event Action<GameState> OnInitialized;
    public static GameState Instance { get; private set; }
    public Vector2Int mazeSize = new (10,10);
    public Vector2Int mazeGrowth = new (2, 2);
    public PlayerData playerData;
    public SaveData saveData;

    public event Action OnLevelEnd;
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerData = new PlayerData();

        saveData = SaveService.LoadJSON();
        OnInitialized?.Invoke(this);
    }

    public void StartLevel()
    {
        playerData = new PlayerData();
        SceneManager.LoadScene("GameScene");
        TimeService.SilentResume();
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void EndLevel()
    {
        TimeService.SilentPause();
        saveData.mazesSolved += 1;
        if (saveData.largestMazeSolved.sqrMagnitude > mazeSize.sqrMagnitude)
        {
            saveData.largestMazeSolved = mazeSize;
        }
        mazeSize += mazeGrowth;
        UIController.instance.ShowEndMenu();
        SaveService.SaveJSON(saveData);
        OnLevelEnd?.Invoke();
    }

    public void OnApplicationQuit()
    {
        SaveService.SaveJSON(saveData);
    }
}

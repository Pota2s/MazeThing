using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
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

    }

    public void StartLevel()
    {
        SceneManager.LoadScene(0);
        TimeService.SilentResume();
        playerData = new PlayerData();
    }

    public void ExitLevel()
    {
        print("Exitting level");
        SceneManager.LoadScene(1);
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
        OnLevelEnd?.Invoke();
    }

    public void OnApplicationQuit()
    {
        SaveService.SaveJSON(saveData);
    }
}

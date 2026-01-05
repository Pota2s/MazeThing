using TMPro;
using UnityEngine;

public class StatisticMenu : MonoBehaviour
{
    private GameState gameState;
    [SerializeField] private TMP_Text mazeSolvedRecord;
    [SerializeField] private TMP_Text mazeSizeRecord;
    private void Awake()
    {
        if (GameState.Instance != null) OnGameStateInitialized(GameState.Instance);
        else GameState.OnInitialized += OnGameStateInitialized;
    }

    private void OnGameStateInitialized(GameState gameState)
    {
        this.gameState = gameState;
        mazeSizeRecord.text = $"Largest Maze : {gameState.saveData.largestMazeSolved.ToString()}";
        mazeSolvedRecord.text = $"Mazes Solved : {gameState.saveData.mazesSolved}";



    }


}

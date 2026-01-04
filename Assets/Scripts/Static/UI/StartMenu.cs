using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Vector2IntInputField sizeField;
    [SerializeField] private Vector2IntInputField growthField;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private const string mazeSize = "MazeSize";
    private const string mazeGrowth = "MazeGrowth";
    private Vector2Int GetVector2Int(string key, Vector2Int fallback) 
    { 
        int x = PlayerPrefs.GetInt($"{key}X");
        int y = PlayerPrefs.GetInt($"{key}Y");

        return new(x, y);
    }

    private void SetVector2Int(string key,Vector2Int value)
    {
        PlayerPrefs.SetInt($"{key}X",value.x);
        PlayerPrefs.SetInt($"{key}Y",value.y);
    }

    void OnEnable()
    {
        startButton.onClick.AddListener(LoadGameScene);
        exitButton.onClick.AddListener(Exit);

        sizeField.SetValue(GetVector2Int(mazeSize, sizeField.GetFallbackVector()));
        growthField.SetValue(GetVector2Int(mazeGrowth, growthField.GetFallbackVector()));
        
        sizeField.OnValueEdited += OnSizeEdited;
        growthField.OnValueEdited -= OnGrowthEdited;
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(LoadGameScene);
        exitButton.onClick.RemoveListener(Exit);

        sizeField.OnValueEdited -= OnSizeEdited;
        growthField.OnValueEdited -= OnGrowthEdited;
    }

    private void OnSizeEdited(Vector2Int newValue)
    {
        SetVector2Int("MazeSize", newValue);
    }

    private void OnGrowthEdited(Vector2Int newValue)
    {
        SetVector2Int("MazeGrowth", newValue);
    }


    void LoadGameScene()
    {
        GameState gameState = GameState.Instance;
        sizeField.GetValue(out Vector2Int size);
        growthField.GetValue(out Vector2Int growth);

        gameState.mazeSize = size;
        gameState.mazeGrowth = growth;

        gameState.StartLevel();
    }

    void Exit()
    {
        Application.Quit();
    }
}

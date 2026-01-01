using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public PlayerData playerData;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerData = new PlayerData();
    }
}

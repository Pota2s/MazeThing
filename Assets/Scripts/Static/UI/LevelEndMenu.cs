using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndMenu : MonoBehaviour
{

    [SerializeField] private Button exitButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text sizeText;
    [SerializeField] private TMP_Text growthText;

    private void OnEnable()
    {
        Vector2Int size = GameState.Instance.mazeSize;
        Vector2Int next = size + GameState.Instance.mazeGrowth;
        sizeText.text = $"Solved : {size.x} x {size.y}";
        growthText.text = $"Next   : {next.x} x {next.y}";

        nextButton.onClick.AddListener(GameState.Instance.StartLevel);
        exitButton.onClick.AddListener(GameState.Instance.ExitLevel);
    }

    private void OnDisable()
    {
        nextButton.onClick?.RemoveListener(GameState.Instance.StartLevel);
        exitButton.onClick?.RemoveListener(GameState.Instance.ExitLevel);
    }

    private void ShowMenu()
    {
        gameObject.SetActive(true);
    }
    private void HideMenu()
    {
        gameObject.SetActive(false);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button exitButton;

    private void OnEnable()
    { 
        continueButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(GameState.Instance.ExitLevel);
    }

    private void OnDisable()
    {
        continueButton.onClick?.RemoveListener(ResumeGame);
        exitButton.onClick?.RemoveListener(GameState.Instance.ExitLevel);
    }

    void ResumeGame()
    {
        TimeService.Resume();
        gameObject.SetActive(false);
    }

    void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    void HideMenu()
    {
        gameObject.SetActive(false);
    }
}

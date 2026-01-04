using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject levelEndMenu;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(BindEvents());
    }

    private IEnumerator BindEvents()
    {
        yield return new WaitUntil(() => InputService.Instance != null);

        InputService.Instance.OnPause += PauseGame;
        TimeService.OnPause += ShowEscapeMenu;
        TimeService.OnResume += HideEscapeMenu;

        GameState.Instance.OnLevelEnd += ShowEndMenu;
    }

    private void OnDisable()
    {
        InputService.Instance.OnPause -= PauseGame;
        TimeService.OnPause -= ShowEscapeMenu;
        TimeService.OnResume -= HideEscapeMenu;

        GameState.Instance.OnLevelEnd -= ShowEndMenu;
    }

    private void ShowEscapeMenu()
    {
        escapeMenu.SetActive(true);
    }

    private void HideEscapeMenu()
    {
        escapeMenu.SetActive(true);
    }

    private void ShowEndMenu()
    {
        levelEndMenu.SetActive(true);
    }

    private void PauseGame()
    {
        TimeService.Toggle();
    }
}

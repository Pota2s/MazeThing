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
        TimeService.OnPause += ShowEscapeMenu;
        TimeService.OnResume += HideEscapeMenu;
    }

    private void OnDisable()
    {
        
        TimeService.OnPause -= ShowEscapeMenu;
        TimeService.OnResume -= HideEscapeMenu;
    }

    private void ShowEscapeMenu()
    {
        escapeMenu.SetActive(true);
    }

    private void HideEscapeMenu()
    {
        escapeMenu.SetActive(true);
    }

    public void ShowEndMenu()
    {
        levelEndMenu.SetActive(true);
    }

    private void PauseGame()
    {
        TimeService.Toggle();
    }
}

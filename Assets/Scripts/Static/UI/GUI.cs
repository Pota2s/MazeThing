using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{

    private PlayerData playerData;

    [SerializeField] GameObject heartContainer;
    [SerializeField] GameObject heartPrefab;
    private List<GameObject> hearts;
    [SerializeField] TMP_Text shieldText;
    [SerializeField] TMP_Text arrowText;

    private void Awake()
    {
        hearts = new List<GameObject>();
    }

    private void OnEnable()
    {
        playerData = GameState.Instance.playerData;

        Setup();

        playerData.OnHealthChanged += SetHealthGraphic;
        playerData.OnArrowChanged += SetArrowText;
        playerData.OnShieldChanged += SetShieldText;

        SetHealthGraphic(playerData.Health);
        SetArrowText(playerData.Health);
        SetShieldText(playerData.Shields);
    }

    private void OnDisable()
    {
        playerData.OnHealthChanged -= SetHealthGraphic;
        playerData.OnArrowChanged -= SetArrowText;
        playerData.OnShieldChanged -= SetShieldText;
    }

    void Setup()
    {
        for (int i = 0; i < playerData.MaxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, Vector3.zero, Quaternion.identity);
            heart.transform.SetParent(heartContainer.transform);
            hearts.Add(heart);
        }
    }

    private void SetHealthGraphic(int value)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < value)
            {
                hearts[i].SetActive(true);
            } else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    private void SetArrowText(int value)
    {
        arrowText.text = value.ToString();
    }

    private void SetShieldText(int value)
    {
        shieldText.text += value.ToString();
    }

}

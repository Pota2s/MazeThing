using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer tokenBase;
    [SerializeField] private SpriteRenderer swordIcon;
    [SerializeField] private SpriteRenderer shieldIcon;
    [SerializeField] private SpriteRenderer bowIcon;
    [SerializeField] private SpriteRenderer windIcon;

    [SerializeField] private List<Color> colorList;
    private PlayerData playerData;
    private Coroutine blinkCoroutine;
    public void Initialize(PlayerData playerData)
    {
        this.playerData = playerData;
        playerData.OnStateChanged += StateChangeHandler;
        playerData.OnArrowChanged += ArrowChangeHandler;
        playerData.OnShieldChanged += ShieldChangeHandler;
        playerData.OnHealthChanged += HealthChangeHandler;

        StateChangeHandler(playerData.State);
        ArrowChangeHandler(playerData.Arrows);
        ShieldChangeHandler(playerData.Shields);
        HealthChangeHandler(playerData.Health);
    }

    private void ShieldChangeHandler(int value)
    {
        if (value < 0)
        {
            Debug.LogError("Negative shield count detected.");
            return;
        }
        if (value >= colorList.Count)
        {
            shieldIcon.color = Color.white;
            return;
        }

        shieldIcon.color = colorList[value];
    }

    private void ArrowChangeHandler(int value)
    {
        if (value < 0) {
            Debug.LogError("Negative arrow count detected.");
            return;
        }
        if (value >= colorList.Count)
        {
            bowIcon.color = Color.white;
            return;
        }

        bowIcon.color = colorList[value];
    }
    private void StateChangeHandler(PlayerState state)
    {
        swordIcon.enabled = false;
        shieldIcon.enabled = false;
        bowIcon.enabled = false;
        windIcon.enabled = false;

        switch (state)
        {
            case PlayerState.Attacking:
                swordIcon.enabled = true;
                break;
            case PlayerState.Defending:
                shieldIcon.enabled = true; 
                break;
            case PlayerState.Shooting:
                bowIcon.enabled = true;
                break;
            case PlayerState.Running:
                windIcon.enabled = true;
                break;
            default:
                swordIcon.enabled = true;
                Debug.LogError("State is not valid, defaulting to attacking.");
                break;
        
        }

    }

    private void HealthChangeHandler(int value)
    {
        if (value <= 1)
        {
            blinkCoroutine = StartCoroutine(Blink());
        } else
        {
            StopCoroutine(blinkCoroutine);
        }
    }

    private IEnumerator Blink()
    {
        Color c = tokenBase.color;
        while (true) {
            c.a = 0.8f;
            tokenBase.color = c;
            yield return new WaitForSeconds(1f);
            c.a = 1f;
            tokenBase.color = c;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDestroy()
    {
        playerData.OnStateChanged -= StateChangeHandler;
        playerData.OnArrowChanged -= ArrowChangeHandler;
        playerData.OnShieldChanged -= ShieldChangeHandler;
        playerData.OnHealthChanged -= HealthChangeHandler;
    }
}

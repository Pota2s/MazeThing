using System;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InputService.Instance.OnInteract += GameState.Instance.EndLevel;
        }
    }

    private void RemoveListener()
    {
        if (InputService.Instance != null) InputService.Instance.OnInteract -= GameState.Instance.EndLevel;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RemoveListener();
        }
    }

    private void OnDisable()
    {
        RemoveListener();
    }
}

using System;
using TMPro;
using UnityEngine;

public class Vector2IntInputField : MonoBehaviour
{
    public event Action<Vector2Int> OnValueEdited;

    [SerializeField] private TMP_InputField xInputField;
    [SerializeField] private TMP_InputField yInputField;
    [Header("Bounds")]
    [SerializeField] private Vector2Int fallbackValues;
    [SerializeField] private Vector2Int minValues;
    [SerializeField] private Vector2Int maxValues;
    [Header("Strict Enforcement")]
    [SerializeField] private bool enforceX;
    [SerializeField] private bool enforceY;

    public void OnEnable()
    {
        xInputField.onEndEdit.AddListener(OnValueChangedX);
        yInputField.onEndEdit.AddListener(OnValueChangedY);
    }

    public void OnDisable()
    {
        xInputField.onEndEdit.RemoveListener(OnValueChangedX);
        yInputField.onEndEdit.RemoveListener(OnValueChangedY);
    }

    private void OnValueChangedX(string text)
    {
        if (enforceX)
        {
            bool success = int.TryParse(text,out int x);
            if (!success)
            {
                xInputField.text = fallbackValues.x.ToString();
                return;
            }

            if (x < minValues.x)
            {
                xInputField.text = minValues.x.ToString();
            }
        
            if (x > maxValues.x)
            {
                xInputField.text = maxValues.x.ToString();
            }
        }

        GetValue(out Vector2Int result);
        OnValueEdited?.Invoke(result);
    }

    private void OnValueChangedY(string text)
    {
        if (enforceY)
        {
            bool success = int.TryParse(text, out int y);
            if (!success)
            {
                yInputField.text = fallbackValues.y.ToString();
                return;
            }

            if (y < minValues.y)
            {
                yInputField.text = minValues.y.ToString();
            }

            if (y > maxValues.y)
            {
                yInputField.text = maxValues.y.ToString();
            }
        }


        GetValue(out Vector2Int result);
        OnValueEdited?.Invoke(result);
    }

    public void SetValue(Vector2Int value)
    {
        xInputField.text = value.x.ToString();
        yInputField.text = value.y.ToString();
    }

    public bool GetValue(out Vector2Int vector)
    {
        bool xSuccess = int.TryParse(xInputField.text, out int x);
        bool ySuccess = int.TryParse(yInputField.text, out int y);

        if (!xSuccess) x = fallbackValues.x;
        if (!ySuccess) y = fallbackValues.y;

        x = Mathf.Clamp(x, minValues.x, maxValues.x);
        y = Mathf.Clamp(y, minValues.y, maxValues.y);

        vector = new (x, y);

        return xSuccess && ySuccess;
    }
    
    public Vector2Int GetFallbackVector()
    {
        return fallbackValues;
    }
}

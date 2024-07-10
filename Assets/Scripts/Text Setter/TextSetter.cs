using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetter : MonoBehaviour
{
    public TMP_Text uiText;

    public void SetText(string newText)
    {
        if (uiText != null)
        {
            uiText.text = newText;
        }
        else
        {
            Debug.LogWarning("TextMesh Pro component is not assigned.");
        }
    }
}

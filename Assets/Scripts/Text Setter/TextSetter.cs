using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSetter : MonoBehaviour
{
    [System.Serializable]
    public struct TextEntry
    {
        public TMP_Text uiText;
        public string text;
    }

    public List<TextEntry> textEntries;

    public void SetTexts()
    {
        foreach (var entry in textEntries)
        {
            if (entry.uiText != null)
            {
                entry.uiText.text = entry.text;
            }
            else
            {
                Debug.LogWarning("TextMesh Pro component is not assigned.");
            }
        }
    }

    public void SetText(int index, string newText)
    {
        if (index >= 0 && index < textEntries.Count)
        {
            if (textEntries[index].uiText != null)
            {
                TextEntry entry = textEntries[index];
                entry.uiText.text = newText;
                entry.text = newText;
                textEntries[index] = entry; // Update the stored text entry
            }
            else
            {
                Debug.LogWarning("TextMesh Pro component is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid index.");
        }
    }
}

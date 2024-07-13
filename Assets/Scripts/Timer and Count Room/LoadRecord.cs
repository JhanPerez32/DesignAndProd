using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRecord : MonoBehaviour
{
    [SerializeField] TextSetter textSetter;

    private void Start()
    {
        DisplayRecords();
    }

    void DisplayRecords()
    {
        // Load the saved best time value
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        int bestRoomCount = PlayerPrefs.GetInt("BestRoomCount", 0);


        // Format the best time value as minutes, seconds, and milliseconds
        string formattedBestTime = FormatTime(bestTime);
        string bestRoomCountText = "Best Room Count: " + bestRoomCount;

        // Display the formatted best time using TextSetter
        if (textSetter != null)
        {
            textSetter.SetText(0, "Best Time: " + formattedBestTime);
            textSetter.SetText(1, bestRoomCountText);
        }
        else
        {
            Debug.LogWarning("TextSetter component is not assigned.");
        }
    }

    private string FormatTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = (time * 1000) % 1000;
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void ClearRecords()
    {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.DeleteKey("BestRoomCount");
        PlayerPrefs.Save();
        DisplayRecords(); 
    }
}

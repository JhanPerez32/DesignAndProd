using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurviveLength : MonoBehaviour
{
    [SerializeField] TextSetter textSetter;

    private bool isRunning;
    private float currentTime;
    private float bestTime;

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        currentTime = 0f;

        if (textSetter != null)
        {
            textSetter.SetTexts(); // Initialize texts if needed
            UpdateTimerText();
        }
        else
        {
            Debug.LogWarning("TextSetter is not assigned.");
        }

        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StopTimer()
    {
        isRunning = false;

        if (currentTime > bestTime || bestTime == 0f)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }
    }

    //For Erasing the Saved Record
    public void DeleteSavedTimer()
    {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();

        bestTime = 0f;
    }

    void UpdateTimerText()
    {
        float currentMinutes = Mathf.FloorToInt(currentTime / 60);
        float currentSeconds = Mathf.FloorToInt(currentTime % 60);
        float currentMilliseconds = (currentTime * 1000) % 1000;
        string currentTimerText = string.Format("{0:00}:{1:00}:{2:000}", currentMinutes, currentSeconds, currentMilliseconds);

        float bestMinutes = Mathf.FloorToInt(bestTime / 60);
        float bestSeconds = Mathf.FloorToInt(bestTime % 60);
        float bestMilliseconds = (bestTime * 1000) % 1000;
        string bestTimerText = string.Format("{0:00}:{1:00}:{2:000}", bestMinutes, bestSeconds, bestMilliseconds);

        // Update the text using TextSetter
        string timerText = string.Format("Current Time: {0}\nBest Time: {1}", currentTimerText, bestTimerText);
        textSetter.SetText(0, timerText);
    }
}

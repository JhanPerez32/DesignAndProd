using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider LoadingBarFill;

    public void LoadScene(string sceneID)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(string sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.value = progressValue;
            progressText.text = (progressValue * 100).ToString("0") + "%";

            yield return null;
        }
    }
}

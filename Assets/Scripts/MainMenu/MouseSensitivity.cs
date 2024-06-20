using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    public Slider mouseSlider;
    public TextMeshProUGUI sliderText;
    float mouseSensitivitySettings;

    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            mouseSensitivitySettings = PlayerManager.Instance.mouseLookSensitivity;
        }
        else
        {
            Debug.LogError("Player Manager Instance is Null");
        }

        if (!PlayerPrefs.HasKey("currentSensitivity"))
        {
            PlayerPrefs.SetFloat("currentSensitivity", 1);
        }

        Load();

        mouseSlider.value = mouseSensitivitySettings / 10;
        sliderText.text = (mouseSlider.value * 100).ToString("0") + "%";

        mouseSlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = (v * 100).ToString("0") + "%";
            AdjustSensitivity(v);
        });
    }

    public void AdjustSensitivity(float newMouseSpeed)
    {
        mouseSensitivitySettings = newMouseSpeed * 100;
        PlayerManager.Instance.mouseLookSensitivity = mouseSensitivitySettings;
        Save();
    }

    private void Load()
    {
        mouseSensitivitySettings = PlayerPrefs.GetFloat("currentSensitivity", 1);
        mouseSlider.value = mouseSensitivitySettings / 10;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("currentSensitivity", mouseSlider.value);
    }
}

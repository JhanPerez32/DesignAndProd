using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    [SerializeField] LoadingManager loadingManager;
    public CharacterController characterController;
    [SerializeField] GameObject playerGameObject;

    private void Start()
    {
        if (playerGameObject != null)
        {
            characterController = playerGameObject.GetComponent<CharacterController>();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        characterController.enabled = false;

        if (loadingManager != null)
        {
            loadingManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

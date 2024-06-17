using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public PlayerMove playerMoveScript;
    public CharacterController characterController;
    public GameObject gameOverScreen;

    [SerializeField] LoadingManager loadingManager;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerMoveScript = GetComponent<PlayerMove>();
        loadingManager = FindObjectOfType<LoadingManager>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        playerMoveScript.enabled = false; // Add this line to disable PlayerMove script
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        characterController.enabled = false;

        if (loadingManager != null)
        {
            loadingManager.LoadScene(SceneManager.GetActiveScene().name); // Uses the LoadingManager to load the Current scene
        }
        else
        {
            Debug.LogError("LoadingManager not found in the scene.");
        }
    }
}

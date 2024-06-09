using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject gameOverScreen;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
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
        Time.timeScale = 0;
        ShowGameOverScreen();
        Debug.Log("Player Died");
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public PlayerMove playerMoveScript;
    public GameObject gameOverScreen;

    void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
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
        playerMoveScript.enabled = false; //Disable the PlayerMove script
    }
}

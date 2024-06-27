using System.Collections;
using System.Collections.Generic;
using Pathways;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] float distanceThreshold;
    [SerializeField] PlayerMove playerMoveScript;
    //[SerializeField] GameObject gameOverScreen;

    public UnityEvent Hit;
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
        if (!enemyController)
        {
            enemyController = GetComponent<EnemyController>();
        }
    }

    private void Update()
    {
        if(enemyController.distanceToPlayer < distanceThreshold)
        {
            Die();
            Debug.Log("Enemy Hit");

        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //gameOverScreen.SetActive(true);
        playerMoveScript.enabled = false; //Disable the PlayerMove script
        Hit.Invoke();
    }
}

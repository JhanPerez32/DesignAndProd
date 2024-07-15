using System.Collections;
using System.Collections.Generic;
using Pathways;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    public float distanceThreshold;
    [SerializeField] PlayerMove playerMoveScript;

    public UnityEvent Hit;

    bool hasKilled;
    public static bool isGameOver;

    void Start()
    {
        hasKilled = false;
        isGameOver = false;

        playerMoveScript = GetComponent<PlayerMove>();

        if (!enemyController)
        {
            enemyController = GetComponent<EnemyController>();
        }
    }

    private void Update()
    {
        if (!hasKilled)
        {
            EnemyHit();
        }
    }

    void EnemyHit()
    {
        if (enemyController.distanceToPlayer < distanceThreshold)
        {
            Die();
            Debug.Log("Player Hit");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hasKilled && hit.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        hasKilled = true;
        isGameOver = true;
        playerMoveScript.enabled = false; //Disable the PlayerMove script
        Debug.Log("Player died, time scale set to 0");
        Hit.Invoke();
    }
}

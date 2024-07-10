using System.Collections;
using System.Collections.Generic;
using Pathways;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] float distanceThreshold;
    [SerializeField] PlayerMove playerMoveScript;

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
        EnemyHit();
    }

    void EnemyHit()
    {
        if (enemyController.distanceToPlayer < distanceThreshold)
        {
            Die();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            Debug.Log("Enemy Hit");
=======
            Debug.Log("Player Hit");
>>>>>>> Stashed changes
=======
            Debug.Log("Player Hit");
>>>>>>> Stashed changes
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
        playerMoveScript.enabled = false; //Disable the PlayerMove script
        Debug.Log("Player died, time scale set to 0");
        Hit.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Pathways
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private GameObject player;
        [SerializeField] public float distanceToPlayer;
        private RoomSpawnRestructure roomSpawn;

        // Adjust these parameters to control the speed behavior
        public float baseSpeed = 5f;  // Base speed of the enemy
        public float maxSpeed = 10f;  // Maximum speed when far from player
        public float distanceThreshold = 10f;  // Distance threshold to start increasing speed

        private void Awake()
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
            roomSpawn = FindObjectOfType<RoomSpawnRestructure>(); // Assuming there's only one RoomSpawnRestructure in the scene
        }

        private void Start()
        {
            if (player == null)
            {
                Debug.LogError("Player not found! Make sure the player has a 'Player' tag assigned.");
                return;
            }
        }

        private void Update()
        {
            // Set destination to player's position
            agent.SetDestination(player.transform.position);

            // Calculate distance between enemy and player
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Adjust agent speed based on distance
            if (distanceToPlayer > distanceThreshold)
            {
                float speed = maxSpeed;
                 
                // Set the agent's speed
                agent.speed = speed;
            }
            else
            {
                // Set back to base speed if within distance threshold
                agent.speed = baseSpeed;
            }
        }
    }
}

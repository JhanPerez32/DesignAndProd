using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;


public class RoomSpawner : MonoBehaviour
{
    [Tooltip("How many Rooms will be spawn as the Game Start")]
    public int distanceToSpawn;

    public List<GameObject> roomPrefabs;
    Vector3 nextSpawnPoint;

    private Quaternion previousSpawnPointRotation = Quaternion.identity;

    public int numberOfSpawnPointsPerRoom = 3;


    private void Start()
    {
        for (int i = 0; i < distanceToSpawn; i++)
        {
            SpawnRoom();
        }
    }

    public void SpawnRoom()
    {
        int randomIndex = Random.Range(0, roomPrefabs.Count);
        GameObject roomToSpawn = roomPrefabs[randomIndex];

        BoxCollider roomCollider = roomToSpawn.GetComponent<BoxCollider>();

        if (roomCollider != null)
        {
            Vector3 spawnPosition = nextSpawnPoint;
            Bounds bounds = roomCollider.bounds;

            Collider[] colliders = Physics.OverlapBox(spawnPosition + bounds.center - roomToSpawn.transform.position, bounds.extents / 2, Quaternion.identity);

            //If this was in == the rooms following room will not Spawn, need help on fixing this, don't know if this was the Problem or the Prefab Object
            if (colliders.Length >= 0)
            {
                GameObject spawnedRoom = Instantiate(roomToSpawn, spawnPosition, Quaternion.identity);

                int randomSpawnPointIndex = Random.Range(0, numberOfSpawnPointsPerRoom);

                if (spawnedRoom.transform.childCount >= numberOfSpawnPointsPerRoom)
                {
                    Transform selectedSpawnPoint = spawnedRoom.transform.GetChild(randomSpawnPointIndex);

                    spawnedRoom.transform.rotation = previousSpawnPointRotation;
                    nextSpawnPoint = selectedSpawnPoint.position;
                    previousSpawnPointRotation = selectedSpawnPoint.rotation;

                    DestroySpawnPoints(spawnedRoom);
                }
                else
                {
                    Debug.LogWarning("Spawned room does not have enough child spawn points. Please check the room prefab.");
                    Destroy(spawnedRoom);
                }
            }
            else
            {
                Debug.LogWarning("Room collision detected. Cannot spawn room at the specified position.");
            }
        }
        else
        {
            Debug.LogWarning("BoxCollider not found on room prefab. Please check the prefab setup.");
        }
    }

    private void DestroySpawnPoints(GameObject spawnedRoom)
    {
        for (int i = 0; i < spawnedRoom.transform.childCount; i++)
        {
            Transform child = spawnedRoom.transform.GetChild(i);
            if (child.CompareTag("SpawnPoint"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}

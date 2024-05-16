using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject room;
    Vector3 nextSpawnPoint;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnRoom();
        }
    }

    public void SpawnRoom()
    {
        GameObject spawn = Instantiate(room, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = spawn.transform.GetChild(1).transform.position;
    }
}

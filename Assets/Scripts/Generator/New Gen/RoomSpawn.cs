using System.Collections.Generic;
using UnityEngine;

namespace Pathways
{

    public class RoomSpawn : MonoBehaviour
    {
        [SerializeField] int roomStartCount = 10;
        [SerializeField] int minStarightRooms = 3;
        [SerializeField] int maxStarightRooms = 10;
        [SerializeField] List<GameObject> straightRooms;
        [SerializeField] List<GameObject> turnRoom;

        private Vector3 currentRoomLoc = Vector3.zero;
        private Vector3 currentRoomDir = Vector3.forward;
        private GameObject prevRoom;

        private List<GameObject> currentRooms;

        private void Start()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            currentRooms = new List<GameObject>();

            Random.InitState(System.DateTime.Now.Millisecond);

            for (int i = 0; i < roomStartCount; i++)
            {
                SpawnRoom(GetRandomRoom().GetComponent<Paths>());
            }

            SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
        }

        private GameObject GetRandomRoom()
        {
            return straightRooms[Random.Range(0, straightRooms.Count)];
        }

        public void SpawnRoom(Paths paths)
        {
            Quaternion newTileRotation = paths.gameObject.transform.rotation * Quaternion.LookRotation(currentRoomDir, Vector3.up);

            prevRoom = GameObject.Instantiate(paths.gameObject, currentRoomLoc, newTileRotation);
            currentRooms.Add(prevRoom);

            Transform spawnPoint = prevRoom.transform.Find("SpawnPoint");
            if (spawnPoint != null)
            {
                currentRoomLoc = spawnPoint.position;
            }
            else
            {
                return;
                //Debug.LogWarning("No SpawnPoint found in the instantiated GameObject.");
            }
        }

        public void DeletePrevRooms()
        {
            while (currentRooms.Count != 1)
            {
                GameObject room = currentRooms[0];
                currentRooms.RemoveAt(0);
                Destroy(room, 5f);
            }
        }

        public void AddNewDirection(Vector3 direction, Vector3 childPos)
        {
            // Update the current room direction
            currentRoomDir = direction;

            // Delete previous rooms
            DeletePrevRooms();

            // Calculate the new room location based on the direction and the position of the child object
            currentRoomLoc = childPos + direction;

            // Determine the number of rooms to spawn in a straight path
            int currentPathLength = Random.Range(minStarightRooms, maxStarightRooms);
            for (int i = 0; i < currentPathLength; i++)
            {
                SpawnRoom(GetRandomRoom().GetComponent<Paths>());
            }

            // Spawn a turn room at the end of the straight path
            SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
        }

        private GameObject SelectRandomGOFromList(List<GameObject> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[Random.Range(0, list.Count)];
        }
    }
}

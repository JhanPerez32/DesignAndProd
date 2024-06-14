using System.Collections.Generic;
using UnityEngine;

namespace Pathways
{
    public class RoomSpawn : MonoBehaviour
    {
        [SerializeField] protected int roomStartCount = 10;
        [SerializeField] protected int minStarightRooms = 3;
        [SerializeField] protected int maxStarightRooms = 10;
        [SerializeField] protected List<GameObject> straightRooms;
        [SerializeField] protected List<GameObject> turnRoom;

        protected Vector3 currentRoomLoc = Vector3.zero;
        protected Vector3 currentRoomDir = Vector3.forward;
        protected GameObject prevRoom;

        protected List<GameObject> currentRooms;

        protected virtual void Start()
        {
            StartSpawn();
        }

        public virtual void StartSpawn()
        {
            currentRooms = new List<GameObject>();

            Random.InitState(System.DateTime.Now.Millisecond);

            for (int i = 0; i < roomStartCount; i++)
            {
                SpawnRoom(GetRandomRoom().GetComponent<Paths>());
            }

            SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
        }

        protected virtual GameObject GetRandomRoom()
        {
            return straightRooms[Random.Range(0, straightRooms.Count)];
        }

        public virtual void SpawnRoom(Paths paths)
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

        public virtual void DeletePrevRooms()
        {
            while (currentRooms.Count != 1)
            {
                GameObject room = currentRooms[0];
                currentRooms.RemoveAt(0);
                Destroy(room, 5f);
            }
        }

        public virtual void AddNewDirection(Vector3 direction, Vector3 childPos)
        {
            currentRoomDir = direction;

            DeletePrevRooms();

            // Calculate the new room location based on the direction and the position of the child object
            currentRoomLoc = childPos + direction;

            int currentPathLength = Random.Range(minStarightRooms, maxStarightRooms);
            for (int i = 0; i < currentPathLength; i++)
            {
                SpawnRoom(GetRandomRoom().GetComponent<Paths>());
            }

            SpawnRoom(SelectRandomGOFromList(turnRoom).GetComponent<Paths>());
        }

        protected GameObject SelectRandomGOFromList(List<GameObject> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[Random.Range(0, list.Count)];
        }
    }
}

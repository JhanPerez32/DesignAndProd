using System.Collections.Generic;
using UnityEngine;

namespace Pathways
{
    public class RoomSpawnRestructure : RoomSpawn
    {
        [System.Serializable]
        public class MainRooms
        {
            public GameObject pathPrefab;
            public PathType pathType;
            [Range(0, 1)] public float spawnChance;
        }

        [System.Serializable]
        public class EventRoom
        {
            public GameObject roomPrefab;
            public int roomEventNumber;
        }

        public enum PathType
        {
            Straight,
            Turn
        }

        [SerializeField] List<MainRooms> mainRooms;
        [SerializeField] List<EventRoom> eventRooms;

        private int totalRoomsSpawned = 0;

        protected override void Start()
        {
            base.Start();
        }

        public override void StartSpawn()
        {
            currentRooms = new List<GameObject>();

            Random.InitState(System.DateTime.Now.Millisecond);

            for (int i = 0; i < roomStartCount; i++)
            {
                SpawnRoom(PickRandomRoom(PathType.Straight).GetComponent<Paths>());
            }

            SpawnRoom(PickRandomRoom(PathType.Turn).GetComponent<Paths>());
        }

        private GameObject PickRandomRoom(PathType pathType)
        {
            List<GameObject> eligibleMainRooms = new List<GameObject>();
            List<GameObject> eligibleDefaultRooms = new List<GameObject>();

            if (pathType == PathType.Straight)
            {
                // Add rooms from the straightRooms list to eligibleDefaultRooms
                eligibleDefaultRooms.AddRange(straightRooms);
            }
            else if (pathType == PathType.Turn)
            {
                // Add rooms from the turnRoom list to eligibleDefaultRooms
                eligibleDefaultRooms.AddRange(turnRoom);
            }

            // Add rooms from mainRooms based on spawnChance and pathType to eligibleMainRooms
            foreach (MainRooms mainRoom in mainRooms)
            {
                if (mainRoom.pathType == pathType && Random.value <= mainRoom.spawnChance)
                {
                    eligibleMainRooms.Add(mainRoom.pathPrefab);
                }
            }

            // Select a random room from the eligible main rooms if available, otherwise from the default rooms
            if (eligibleMainRooms.Count > 0)
            {
                return eligibleMainRooms[Random.Range(0, eligibleMainRooms.Count)];
            }

            // If no main rooms are available, select a room from the default list
            if (eligibleDefaultRooms.Count > 0)
            {
                return eligibleDefaultRooms[Random.Range(0, eligibleDefaultRooms.Count)];
            }

            // Fallback: if no rooms are available in either list, choose from base or fallback list
            return pathType == PathType.Straight ? base.GetRandomRoom() : SelectRandomGOFromList(turnRoom);
        }

        public override void AddNewDirection(Vector3 direction, Vector3 childPos)
        {
            currentRoomDir = direction;

            DeletePrevRooms();

            currentRoomLoc = childPos + direction;

            int currentPathLength = Random.Range(minStarightRooms, maxStarightRooms);
            for (int i = 0; i < currentPathLength; i++)
            {
                SpawnRoom(PickRandomRoom(PathType.Straight).GetComponent<Paths>());
            }

            SpawnRoom(PickRandomRoom(PathType.Turn).GetComponent<Paths>());
        }

        private GameObject PickEventRoom()
        {
            foreach (EventRoom eventRoom in eventRooms)
            {
                if (eventRoom.roomEventNumber == totalRoomsSpawned + 1)
                {
                    totalRoomsSpawned++;
                    return eventRoom.roomPrefab;
                }
            }

            return null;
        }

        private GameObject PickRoom(PathType pathType)
        {
            GameObject eventRoom = PickEventRoom();
            if (eventRoom != null)
                return eventRoom;

            return PickRandomRoom(pathType);
        }
    }
}

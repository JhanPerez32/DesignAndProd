using Pathways;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedRooms : MonoBehaviour
{
    [SerializeField] TextSetter textSetter;
    [SerializeField] RoomSpawnRestructure spawnedRoom;

    private int bestRecord;

    private void Start()
    {
        bestRecord = PlayerPrefs.GetInt("BestRoomCount", 0);

        spawnedRoom.totalRoomsSpawned = 0;
        UpdateRoomCountText();
    }

    private void Update()
    {
        // Update the room count text if totalRoomsSpawned changes
        UpdateRoomCountText();
    }

    private void UpdateRoomCountText()
    {
        int displayedRoomCount = spawnedRoom.totalRoomsSpawned - 1; // Subtract 1 to start from zero
        displayedRoomCount = Mathf.Max(displayedRoomCount, 0);

        string roomCountText = displayedRoomCount + " :Entered Room";

        // It will update the Best Record when the Previous record was beaten
        if (displayedRoomCount > bestRecord)
        {
            bestRecord = displayedRoomCount;
            PlayerPrefs.SetInt("BestRoomCount", bestRecord);
        }

        // Display the best record
        string bestRecordText = bestRecord + " :Best Record";
        string combinedText = roomCountText + "\n" + bestRecordText;

        textSetter.SetText(1, combinedText);
    }
}

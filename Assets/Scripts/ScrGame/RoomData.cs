using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    private static RoomData Instance;
    public int level;
    public string roomName;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        roomName = GetRandomRoomName();
    }

    public static RoomData GetInstance()
    {
        if (Instance == null)
        {
            GameObject GO = new GameObject();
            GO.name = "RoomData";
            GO.AddComponent(typeof(RoomData));
            RoomData.Instance = GO.GetComponent<RoomData>();
        }
        return RoomData.Instance;
    }

    // public RoomData(int level)
    // {
    //     this.level = level;
    //     roomName = level + GetRandomRoomName();
    // }

    private string GetRandomRoomName()
    {
        return Random.Range(100000, 999999).ToString();
    }
}
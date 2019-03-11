﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Private variables
    private GameObject northDoor;
    private GameObject eastDoor;
    private GameObject southDoor;
    private GameObject westDoor;

    // Public variables
    public LevelManager myGenerator;
    public Vector2 virtualLoc;
    public Vector2 size;

    public Room northRoom;
    public Room eastRoom;
    public Room southRoom;
    public Room westRoom;

    public GameObject walls;
    public GameObject doors;
    public GameObject floor;
    public GameObject traps;

    public bool doorsOpen = true;

    public void Init()
    {
        CreateSubTransforms();
    }

    // Level generated now finalize rooms
    public void Finish()
    {
        GetNeighbors();
        CreateDoors();
    }

    private void GetNeighbors()
    {
        Vector2 north = new Vector2(virtualLoc.x, virtualLoc.y + 1);
        Vector2 east = new Vector2(virtualLoc.x + 1, virtualLoc.y);
        Vector2 south = new Vector2(virtualLoc.x, virtualLoc.y - 1);
        Vector2 west = new Vector2(virtualLoc.x - 1, virtualLoc.y);

        if (myGenerator.rooms.ContainsKey(north))
        {
            northRoom = myGenerator.rooms[north];
        }

        if (myGenerator.rooms.ContainsKey(east))
        {
            eastRoom = myGenerator.rooms[east];
        }

        if (myGenerator.rooms.ContainsKey(south))
        {
            southRoom = myGenerator.rooms[south];
        }

        if (myGenerator.rooms.ContainsKey(west))
        {
            westRoom = myGenerator.rooms[west];
        }
    }

    private void CreateDoors()
    {
        // North door generation
        if (northRoom)
        {
            northDoor = Instantiate(myGenerator.normalDoorTop, new Vector2(Mathf.Floor(size.x / 2), size.y), Quaternion.identity, doors.transform);
            northDoor.GetComponent<Door>().room = this;
        }

        if (eastRoom)
        {
            eastDoor = Instantiate(myGenerator.normalDoorRight, new Vector2(size.x, Mathf.Floor(size.y / 2)), Quaternion.identity, doors.transform);
            eastDoor.GetComponent<Door>().room = this;
        }

        if (southRoom)
        {
            southDoor = Instantiate(myGenerator.normalDoorBottom, new Vector2(Mathf.Floor(size.x / 2), -1), Quaternion.identity, doors.transform);
            southDoor.GetComponent<Door>().room = this;
        }

        if (westRoom)
        {
            westDoor = Instantiate(myGenerator.normalDoorLeft, new Vector2(-1, Mathf.Floor(size.y / 2)), Quaternion.identity, doors.transform);
            westDoor.GetComponent<Door>().room = this;
        }
    }

    private void CreateSubTransforms()
    {
        walls = new GameObject("Walls");
        walls.transform.SetParent(transform);

        doors = new GameObject("Doors");
        doors.transform.SetParent(transform);

        floor = new GameObject("Floor");
        floor.transform.SetParent(transform);

        traps = new GameObject("Traps");
        traps.transform.SetParent(transform);
    }

    public void LeaveRoom(GameObject door)
    {
        Room newRoom = null;

        // Find which room
        if (door == northDoor)
        {
            newRoom = northRoom;
        }
        else if (door == eastDoor)
        {
            newRoom = eastRoom;
        }
        else if (door == southDoor)
        {
            newRoom = southRoom;
        }
        else if (door == westDoor)
        {
            newRoom = westRoom;
        }

        // Move to new room
        myGenerator.TransistRoom(this, newRoom);
    }
}

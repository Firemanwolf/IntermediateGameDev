using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up, down, left, right };
    public Direction direction;

    [Header("Room Info")]
    public GameObject roomPrefab;
    public int roomNum;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("Location Control")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public int maxStep;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();

    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType wallType;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNum; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //changing point position
            ChangePointPos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
           // if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
           // {
           //     endRoom = room.gameObject;
           // }

            SetupRoom(room, room.transform.position);
        }

        FindEndRoom();

        endRoom.GetComponent<SpriteRenderer>().color = endColor;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

    public void SetupRoom(Room newRoom, Vector3 roomPos)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPos + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPos + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPos + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPos + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffset, yOffset);

        switch (newRoom.DoorNum)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp, roomPos, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleDown, roomPos, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPos, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPos, Quaternion.identity);
                break;
            case 2:
                if(newRoom.roomLeft && newRoom.roomUp)
                {
                    Instantiate(wallType.doubleLU, roomPos, Quaternion.identity);
                }
                if (newRoom.roomLeft && newRoom.roomRight)
                {
                    Instantiate(wallType.doubleLR, roomPos, Quaternion.identity);
                }
                if (newRoom.roomLeft && newRoom.roomDown)
                {
                    Instantiate(wallType.doubleLD, roomPos, Quaternion.identity);
                }
                if (newRoom.roomUp && newRoom.roomDown)
                {
                    Instantiate(wallType.doubleUD, roomPos, Quaternion.identity);
                }
                if (newRoom.roomUp && newRoom.roomRight)
                {
                    Instantiate(wallType.doubleUR, roomPos, Quaternion.identity);
                }
                if (newRoom.roomUp && newRoom.roomDown)
                {
                    Instantiate(wallType.doubleUD, roomPos, Quaternion.identity);
                }
                if (newRoom.roomRight && newRoom.roomDown)
                {
                    Instantiate(wallType.doubleRD, roomPos, Quaternion.identity);
                }
                break;
            case 3:
                if (!newRoom.roomUp)
                    Instantiate(wallType.tripleLRD, roomPos, Quaternion.identity);
                if (!newRoom.roomRight)
                    Instantiate(wallType.tripleLUD, roomPos, Quaternion.identity);
                if (!newRoom.roomDown)
                    Instantiate(wallType.tripleLUR, roomPos, Quaternion.identity);
                if(!newRoom.roomLeft)
                    Instantiate(wallType.tripleURD, roomPos, Quaternion.identity);
                break;
            case 4:
                if(newRoom.roomUp && newRoom.roomDown && newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.quadraDoors, roomPos, Quaternion.identity);
                break;
        }
    }

    public void FindEndRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //Attain maximumValueRoom and secondMaximumValueRoom
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().DoorNum == 1)
                oneWayRooms.Add(farRooms[i]);
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().DoorNum == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }
}

[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleDown,
                      doubleLU, doubleLR, doubleLD, doubleUD, doubleUR, doubleRD,
                      tripleLUR, tripleLUD, tripleURD, tripleLRD,
                      quadraDoors;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public GameObject doorRight, doorLeft, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;

    public int stepToStart;

    public int DoorNum;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    public void UpdateRoom()
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));
        text.text = stepToStart.ToString();

        if (roomUp) DoorNum++;
        if(roomRight) DoorNum++;
        if (roomLeft) DoorNum++;
        if (roomDown) DoorNum++;

    }
}

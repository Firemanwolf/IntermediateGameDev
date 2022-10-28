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
    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        text.text = stepToStart.ToString();

        if (roomUp) DoorNum++;
        if(roomRight) DoorNum++;
        if (roomLeft) DoorNum++;
        if (roomDown) DoorNum++;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }
}

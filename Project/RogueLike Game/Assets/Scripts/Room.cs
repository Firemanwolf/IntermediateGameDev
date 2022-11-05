using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public GameObject doorRight, doorLeft, doorUp, doorDown;
    public bool roomLeft, roomRight, roomUp, roomDown;

    bool cleared = false;
    public bool Final = false;

    public int stepToStart;

    public int DoorNum;

    public GameObject EnemyPrefab,Wall;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
        if (Final && Wall) Wall.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
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
            if (!cleared)
            {
                for (int i = 0; i < Random.Range(1, 3); i++)
                {
                    Instantiate(EnemyPrefab, new Vector3(Random.Range(transform.position.x + 16 / 2 - 1, transform.position.x - 16 / 2),
                    Random.Range(transform.position.y + 8 / 2 - 1, transform.position.y - 8 / 2),
                    transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (doorLeft) doorLeft.GetComponent<Collider2D>().enabled = true;
            if (doorUp) doorUp.GetComponent<Collider2D>().enabled = true;
            if (doorRight) doorRight.GetComponent<Collider2D>().enabled = true;
            if (doorDown) doorDown.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (doorLeft) doorLeft.GetComponent<Collider2D>().enabled = false;
            if (doorUp) doorUp.GetComponent<Collider2D>().enabled = false;
            if (doorRight) doorRight.GetComponent<Collider2D>().enabled = false;
            if (doorDown) doorDown.GetComponent<Collider2D>().enabled = false;
            if (Final) Scenemanager.isWin = true;
        }
        else cleared = true;
    }
}

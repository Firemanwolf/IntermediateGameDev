using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float xImpulse = 2f;
    [SerializeField] float yImpulse = 15f;
    Rigidbody2D _rigid;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
         _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) start = true;
        if (start) playBall();
    }

    void playBall()
    {
        start = false;
        _rigid.velocity = new Vector2(xImpulse, yImpulse);
    }
}

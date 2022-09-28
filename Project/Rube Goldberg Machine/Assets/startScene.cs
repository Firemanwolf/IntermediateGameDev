using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScene : MonoBehaviour
{
    public Rigidbody2D hand;
    public Rigidbody2D ball;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hand.bodyType = RigidbodyType2D.Dynamic;
            ball.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

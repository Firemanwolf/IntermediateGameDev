using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movingSpeed;
    public float limitLeft;
    public float limitRight;
    Vector2 current_pos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        current_pos = new Vector2(transform.position.x,transform.position.y);
        current_pos.x +=Input.GetAxisRaw("Horizontal") * movingSpeed * Time.deltaTime;
        current_pos.x = Mathf.Clamp(current_pos.x, limitLeft, limitRight);
        transform.position = current_pos;
    }
}

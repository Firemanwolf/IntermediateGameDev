using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public Transform ball;
    public Transform goal;
    public Vector2 addedForce;
    public GameObject Pivot;
    public Rigidbody2D Jab;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
         sprite = Pivot.GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if(ball.position.x >= goal.position.x)
        {
            sprite.color = Color.red;
            Jab.AddForce(addedForce);
            Debug.Log(1);
        }
    }
}

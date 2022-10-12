using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_ : MonoBehaviour
{
    public int HP = 1;
    private void Update()
    {
        if (HP == 0)Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")HP-=1;
    }
}

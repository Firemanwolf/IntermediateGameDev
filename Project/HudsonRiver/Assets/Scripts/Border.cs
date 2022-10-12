using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Border : MonoBehaviour
{
    public UnityEvent HitEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            collision.rigidbody.bodyType = RigidbodyType2D.Static;
            Destroy(collision.gameObject);
        }
    }
}

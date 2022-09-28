using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escalator : MonoBehaviour
{
   public Transform ball;
   public Transform goal;
   private Rigidbody2D _rigidbody;
    public GameObject jab;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.position.x >= goal.position.x) 
        { 
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            jab.SetActive(false);
        }
    }
}

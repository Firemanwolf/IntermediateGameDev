using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BackgroundControl : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigid.MovePosition(target.position + offset * Time.deltaTime);
    }
}
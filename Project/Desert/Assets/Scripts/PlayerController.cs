using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    public Vector2 movement;

    public Animator playerAnimator;
    public bool facingLeft = true;
    public Camera _camera;
    public float expandSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            _camera.orthographicSize += expandSpeed * Time.deltaTime;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 0, 40);
            if (movement.x != 0) playerAnimator.SetBool("movingHorizontal", true);
        }else playerAnimator.SetBool("movingHorizontal", false);
        if (movement.x > 0 && facingLeft) flip();
        if(movement.x < 0 && !facingLeft) flip();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed);

    }

    void flip()
    {
        Vector3 CurrentScale= gameObject.transform.localScale;
        CurrentScale.x *= -1;
        gameObject.transform.localScale = CurrentScale;
        facingLeft = !facingLeft;
    }
}
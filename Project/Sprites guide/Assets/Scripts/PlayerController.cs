using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationDirection
{
    Front,
    Right
}

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    public Vector2 movement;

    public KeyCode jumpKey;

    public Animator playerAnimator;
    private string _currentState;
    private AnimationDirection _playerDirection;

    #region Animation Clips
    public string IDLE_FRONT;
    public string IDLE_RIGHT;

    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        /*
        if (Input.GetKeyDown(jumpKey))
        {
            playerAnimator.SetBool("movingRight", true);

        }

        if (Input.GetKeyUp(jumpKey))
        {
            playerAnimator.SetBool("movingRight", false);
        }*/
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed);


        if (_playerDirection == AnimationDirection.Front)
            ChangeAnimationState(IDLE_FRONT);
        else if (_playerDirection == AnimationDirection.Right)
            ChangeAnimationState(IDLE_RIGHT);

        if (movement.x > 0) //right
        {
            ChangeAnimationState(IDLE_RIGHT);
            _playerDirection = AnimationDirection.Right;
        }
        else if (movement.y < 0) // down
        {
            ChangeAnimationState(IDLE_FRONT);
            _playerDirection = AnimationDirection.Front;
        }

    }

    void ChangeAnimationState(string newState)
    {
        if (_currentState == newState)
            return;

        playerAnimator.Play(newState);


        _currentState = newState;
    }
}
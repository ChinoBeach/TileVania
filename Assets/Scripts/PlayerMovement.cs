using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //variables 
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRidgidBody;

    Animator myAnimator;

    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * fltRunSpeed, myRidgidBody.velocity.y);
        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        myRidgidBody.velocity = playerVelocity;

        myAnimator.SetBool("bolIsRunning", bolPlayerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        if (bolPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidBody.velocity.x), 1f);
        }
        
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            myRidgidBody.velocity += new Vector2 (0f, fltJumpSpeed);
        }
    }
}

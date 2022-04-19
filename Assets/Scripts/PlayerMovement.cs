using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //variables 
    [SerializeField] float fltRunSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D myRidgidBody;
    
    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * fltRunSpeed, myRidgidBody.velocity.y);

        myRidgidBody.velocity = playerVelocity;
    }
}

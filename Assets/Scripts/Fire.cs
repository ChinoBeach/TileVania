using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float fltFireSpeed = 20f;
    Rigidbody2D myRigidbody2D;
    PlayerMovement player;
    float fltXSpeed;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        fltXSpeed = player.transform.localScale.x * fltFireSpeed;
    }

    
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(fltXSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}

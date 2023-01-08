using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fly : MonoBehaviour
{
    Rigidbody2D flyRB;
    Collider2D flyCollider;

    public float flySpeed = 150;

    Vector3 velocity;

    public float sinSpeed = 10;
    public float sinAmplitude = 80;

    public bool canMove = true;
    public bool isAlive = true;
    int axisDirection = 1;

    private void Awake()
    {
        flyRB = GetComponent<Rigidbody2D>();
        flyCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        isAlive = true;
        //What if we want it to not move at all?
        canMove = true;
    }

    private void Update()
    {
        velocity.x = (axisDirection) * flySpeed;
        velocity.y = sinAmplitude * Mathf.Sin(Time.time * sinSpeed);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            flyRB.velocity = velocity * Time.fixedDeltaTime;
        }
    }

    public void DisableMovement() 
    {
        canMove = false;
        flyRB.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Structure"))
        {
            axisDirection *= -1;
        }
    }

    private void OnDisable()
    {
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    private Vector2 Movement;
    private Rigidbody2D rb;
    private CounterManagerScript _counterManagerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        _counterManagerScript = GameObject.Find("CounterManager").GetComponent<CounterManagerScript>();
    }

    void Update()
    {
        // Get input from arrow keys
        Movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.velocity = Movement * MoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collidingTag = other.gameObject.tag;
        switch (collidingTag)
        {
            case "Homebase":
                
                _counterManagerScript.AddSavedSoldiers();
                _counterManagerScript.AddSavedSoldiers();
                
                break;
            case "Obstacle":
                //Gameover Screen
                Debug.Log("Game Over");
                break;
            case "Soldier":
                Destroy(other.gameObject);
                
                _counterManagerScript.AddSoldiersOnBoard();
                
                break;
            default:
                Debug.Log("Not recognised collision");
                break;
        }
    }
    
    
}
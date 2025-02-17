using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    private Vector2 Movement;
    private Rigidbody2D rb;
    protected int SoldiersOnBoard = 0;
    protected int SoldiersSaved = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
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
                //drop soldiers off
                if (SoldiersOnBoard > 0)
                {
                    SoldiersOnBoard = 0;
                    SoldiersSaved += SoldiersOnBoard;
                    break;
                }
                
                Debug.Log("No Soldiers to drop off! Save some soldiers and try again.");
                break;
            case "Obstacle":
                //Gameover Screen
                Debug.Log("Game Over");
                break;
            case "Soldier":
                if (SoldiersOnBoard < 3)
                {
                    Destroy(other.gameObject);
                    SoldiersOnBoard += 1;
                    Debug.Log("Soldier Saved! You have " + SoldiersOnBoard + " soldiers on board now.");
                    break;
                }
                
                Debug.Log("Too many soldiers on board! Take them to the Hospital and try again.");
                break;
            default:
                Debug.Log("Not recognised collision");
                break;
        }
    }
    
    
}
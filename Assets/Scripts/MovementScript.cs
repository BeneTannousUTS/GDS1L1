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
    private UpdateUIContentScript _uiManagerScript;
    private bool isInputDisabled = false;
    private bool isSoldierPickedUp  = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        _counterManagerScript = GameObject.Find("CounterManager").GetComponent<CounterManagerScript>();
        _uiManagerScript = GameObject.Find("UIManager").GetComponent<UpdateUIContentScript>();
    }

    void Update()
    {
        if (!isInputDisabled) // Only allow movement if input is enabled
        {
            // Get input from arrow keys
            Movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }else
        {
            Movement = Vector2.zero; // Stop movement
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.linearVelocity = Movement * MoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collidingTag = other.gameObject.tag;
    
        switch (collidingTag)
        {
            case "Homebase":
                _counterManagerScript.AddSavedSoldiers();
                break;

            case "Obstacle":
                // Gameover Screen
                _uiManagerScript.SetGameOverActiveState(true);
                isInputDisabled = true;
                break;

            case "Soldier":
                if (_counterManagerScript.GetSoldiersOnBoard() < 3 && !isSoldierPickedUp)
                {
                    // Mark the soldier as picked up
                    isSoldierPickedUp = true;

                    // Destroy the soldier after pickup
                    Destroy(other.gameObject);

                    // Add the soldier to the soldiers on board counter
                    _counterManagerScript.AddSoldiersOnBoard();
                    GetComponent<AudioSource>().Play();
                }
                break;

            default:
                Debug.Log("Not recognised collision");
                break;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset the pickup flag when the soldier exits the trigger zone
        if (other.CompareTag("Soldier"))
        {
            isSoldierPickedUp = false;
        }
    }

    public void setInputsDisabled(bool state) 
    {
        isInputDisabled = state;
    }
}
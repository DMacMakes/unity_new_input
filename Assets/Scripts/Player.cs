using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float MAX_SPEED = 2.0f;
    private Vector2 _targetVelocity2d = Vector2.zero; // User's input
    private const int MOUSE_MAX_SPEED = 30;  // How far a mouse move is top speed

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Started.");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        string inputControl = context.action.activeControl.displayName;
        //Debug.Log("Control used:" + inputControl);
        //Debug.Log("Value type: " + value.GetType());
        // If either axis shows movement, we better act.
        if (inputControl == "Delta")
        {
            // Cap any mouse moves at a sensible max movement speed
            // Normalise the result to a 0-1 range 
            moveVector.x = Mathf.Clamp(moveVector.x, -MOUSE_MAX_SPEED, MOUSE_MAX_SPEED);
            moveVector.y = Mathf.Clamp(moveVector.y, -MOUSE_MAX_SPEED, MOUSE_MAX_SPEED);
            moveVector.x /= MOUSE_MAX_SPEED;
            moveVector.y /= MOUSE_MAX_SPEED;
            moveVector = Vector2.ClampMagnitude(moveVector, 1.0f);
            
            // Now normalise the vector again so the max length is 1 (since 1,1 is longer than 1).
            //moveVector.Normalize();
            Debug.Log("Delta x: " + moveVector.x + " y: " + moveVector.y);
        } 
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            _targetVelocity2d.x = moveVector.x * 2.0f;
            _targetVelocity2d.y = moveVector.y * 2.0f;
            Debug.Log("Move:" + moveVector.x + "," + moveVector.y);
        }
        else // begin stopping
        {
            //Debug.Log("Stop!");
            _targetVelocity2d.x = _targetVelocity2d.y = 0;
        }
        
    }
}

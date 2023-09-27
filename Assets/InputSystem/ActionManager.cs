using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;
    public UnityEvent<int> altMoveCheck;
    public UnityEvent<Vector2> aimLook;
    public UnityEvent<bool> fire;
    public UnityEvent firePoint;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("JumpHold was started");
        }
        else if (context.performed)
        {
            Debug.Log("JumpHold was performed");
            Debug.Log(context.duration);
            jumpHold.Invoke();
        }
        else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Jump was started");
        else if (context.performed)
        {
            jump.Invoke();
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
            Debug.Log("Jump was cancelled");

    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
            moveCheck.Invoke(0);
        }
    }

    public void OnAltMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("move started");
            int moveDir = context.ReadValue<float>() > 0? 1 : -1;
            altMoveCheck.Invoke(moveDir);
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
            altMoveCheck.Invoke(0);
        }
    }

    public void OnFireAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("mouse click performed");
            bool mouseHeldDown = context.ReadValueAsButton();
            fire.Invoke(mouseHeldDown);
        }
        else if (context.canceled)
        {
            Debug.Log("mouse click cancelled");
            bool mouseHeldDown = context.ReadValueAsButton();
            fire.Invoke(mouseHeldDown);
        }
            
    }

    public void OnFirePointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            Debug.Log($"Point detected: {point}");
        }
    }

    public void OnAimLookAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePos = context.ReadValue<Vector2>();
            aimLook.Invoke(mousePos); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

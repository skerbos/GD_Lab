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
    public UnityEvent<int> moveVerticalCheck;
    public UnityEvent<Vector2> aimLook;
    public UnityEvent<bool> fire;
    public UnityEvent<bool, Vector2> altFire;
    public UnityEvent firePoint;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            jumpHold.Invoke();
        }
        else if (context.canceled)
        {
            
        }
    }

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }

        else if (context.performed)
        {
            jump.Invoke();
        }
        else if (context.canceled)
        {
            
        }

    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            moveCheck.Invoke(0);
        }
    }

    public void OnAltMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int moveDir = context.ReadValue<float>() > 0? 1 : -1;
            altMoveCheck.Invoke(moveDir);
        }
        if (context.canceled)
        {
            altMoveCheck.Invoke(0);
        }
    }

    public void OnMoveVerticalAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            int moveDir = context.ReadValue<float>() > 0 ? 1 : -1;
            moveVerticalCheck.Invoke(moveDir);
        }

        if (context.canceled)
        {
            moveVerticalCheck.Invoke(0);
        }
    }

    public void OnFireAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool mouseHeldDown = context.ReadValueAsButton();
            fire.Invoke(mouseHeldDown);
        }
        else if (context.canceled)
        {
            bool mouseHeldDown = context.ReadValueAsButton();
            fire.Invoke(mouseHeldDown);
        }
            
    }

    public void OnAltFireAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            altFire.Invoke(true, point);
        }
        else if (context.canceled)
        {
            Vector2 point = context.ReadValue<Vector2>();
            altFire.Invoke(false, point);
        }
    }

    public void OnFirePointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public enum PlayerInputMode 
{
    PIM_Buttons,
    PIM_Minimal,
    PIM_TiltTouch
}

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputs playerInputs;
    public PlayerInputMode playerInputMode = PlayerInputMode.PIM_Minimal;

    //Enhanced Inputs
    public delegate void FingerDownEvent(Vector2 position, float time, int fingerIndex);
    public event FingerDownEvent OnFingerDown;

    public delegate void FingerMoveEvent(Vector2 position, float time, int fingerIndex);
    public event FingerMoveEvent OnFingerMove;

    public delegate void FingerUpEvent(Vector2 position, float time, int fingerIndex);
    public event FingerUpEvent OnFingerUp;

    public virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Enable();

        switch (playerInputMode)
        {
            case PlayerInputMode.PIM_Minimal:

                break;

        }
    }

    public void OnInputModeChange(PlayerInputMode newInputMode)
    {
        //Release
        switch (playerInputMode)
        {
            case PlayerInputMode.PIM_Minimal:

                ReleaseMinimalInputMode();

                break;
            case PlayerInputMode.PIM_Buttons:



                break;
            case PlayerInputMode.PIM_TiltTouch:



                break;
        }

        playerInputMode = newInputMode;

        //Capture new input settings
        switch (newInputMode)
        {
            case PlayerInputMode.PIM_Minimal:

                SetUpMinimalInputMode();

                break;
            case PlayerInputMode.PIM_Buttons:



                break;
            case PlayerInputMode.PIM_TiltTouch:



                break;
        }
    }

    public PlayerInputs GetPlayerInputsClass() 
    {
        return playerInputs;
    }

    //Simple Mobile Input
    //
    //void StartTouch(InputAction.CallbackContext ctx)
    //{
    //    //Debug.Log("Touch Started!");
    //
    //    //if (OnStartTouch != null) 
    //    //{
    //    //    OnStartTouch(playerInputs.Mobile.TouchPosition.ReadValue<Vector2>(), (float)ctx.startTime);
    //    //}
    //}
    //
    //void EndTouch(InputAction.CallbackContext ctx) 
    //{
    //    //Debug.Log("Touch Ended!");
    //
    //    //if (OnEndTouch != null)
    //    //{
    //    //    OnEndTouch(playerInputs.Mobile.TouchPosition.ReadValue<Vector2>(), (float)ctx.time);
    //    //}
    //}

    #region MinimalInputMode

    //Minimal Code

    void SetUpMinimalInputMode() 
    {
        EnhancedTouchSupport.Enable();

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    void FingerDown(Finger finger) 
    {
        //Debug.Log("Touch Started!");

        if (OnFingerDown != null)
        {
            OnFingerDown(finger.screenPosition, Time.time, finger.index);
        }
    }

    void FingerMove(Finger finger)
    {
        //Debug.Log("Touch Moved!");

        if (OnFingerMove != null)
        {
            OnFingerMove(finger.screenPosition, Time.time, finger.index);
        }
    }

    void FingerUp(Finger finger)
    {
        //Debug.Log("Touch Ended!");

        if (OnFingerUp != null)
        {
            OnFingerUp(finger.screenPosition, Time.time, finger.index);
        }
    }

    void ReleaseMinimalInputMode() 
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;

        EnhancedTouchSupport.Disable();
    }

#endregion

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}


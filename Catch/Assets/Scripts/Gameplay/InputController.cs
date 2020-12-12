using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[Serializable]
public class MoveInputEvent : UnityEvent<float, float> { }


public class InputController : MonoBehaviour
{
    public MoveInputEvent moveInputEvent;
    public UnityEvent jumpStartEvent;
    public UnityEvent jumpCancelEvent;
    public UnityEvent boostStartEvent;
    public UnityEvent boostCancelEvent;
    public UnityEvent diveStartEvent;
    public UnityEvent diveCancelEvent;
    public UnityEvent grabStartEvent;
    public UnityEvent grabCancelEvent;
    public UnityEvent throwStartEvent;
    public UnityEvent throwCancelEvent;

    public UnityEvent togglePauseEvent;
    public UnityEvent reloadSceneEvent;
    public UnityEvent toggleInvertLookEvent;

    Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();

        // ***** PLAYER CONTROLS *****
        controls.Gameplay.Move.performed += OnMovePerformed;
        controls.Gameplay.Move.canceled += OnMovePerformed;

        controls.Gameplay.Jump.started += OnJumpStarted;
        controls.Gameplay.Jump.canceled += OnJumpCanceled;

        controls.Gameplay.Boost.started += OnBoostStarted;
        controls.Gameplay.Boost.canceled += OnBoostCanceled;

        controls.Gameplay.Dive.started += OnDiveStarted;
        controls.Gameplay.Dive.canceled += OnDiveCanceled;

        controls.Gameplay.Grab.started += OnGrabStarted;
        controls.Gameplay.Grab.canceled += OnGrabCanceled;

        controls.Gameplay.Throw.started += OnThrowStarted;
        controls.Gameplay.Throw.canceled += OnThrowCanceled;

        // ***** OTHER INPUTS *****
        controls.Gameplay.Pause.started += OnPause;
        controls.Gameplay.ReloadScene.started += OnReloadStarted;
        controls.Gameplay.ToggleInvertLook.started += OnToggleInvertLookStarted;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        jumpStartEvent.Invoke();
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpCancelEvent.Invoke();
    }

    private void OnBoostStarted(InputAction.CallbackContext context)
    {
        boostStartEvent.Invoke();
    }
    private void OnBoostCanceled(InputAction.CallbackContext context)
    {
        boostCancelEvent.Invoke();
    }

    private void OnDiveStarted(InputAction.CallbackContext context)
    {
        diveStartEvent.Invoke();
    }

    private void OnDiveCanceled(InputAction.CallbackContext context)
    {
        diveCancelEvent.Invoke();
    }

    private void OnGrabStarted(InputAction.CallbackContext context)
    {
        grabStartEvent.Invoke();
    }
    private void OnGrabCanceled(InputAction.CallbackContext context)
    {
        grabCancelEvent.Invoke();
    }

    private void OnThrowStarted(InputAction.CallbackContext context)
    {
        throwStartEvent.Invoke();
    }

    private void OnThrowCanceled(InputAction.CallbackContext obj)
    {
        throwCancelEvent.Invoke();
    }


    private void OnReloadStarted(InputAction.CallbackContext context)
    {
        reloadSceneEvent.Invoke();
    }

    private void OnToggleInvertLookStarted(InputAction.CallbackContext context)
    {
        toggleInvertLookEvent.Invoke();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        togglePauseEvent.Invoke();
    }
}

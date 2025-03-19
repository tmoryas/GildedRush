using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Joystick : MonoBehaviour
{
    private PlayerInput input;

    private InputAction touchAction;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        touchAction = input.FindAction("Touch");
    }

    private void OnEnable()
    {
        touchAction.performed += TouchDown;
    }

    private void OnDisable()
    {
        touchAction.performed -= TouchDown;
    }

    private void TouchDown(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        Debug.Log(value);

    }
}

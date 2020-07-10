using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerInputActionAsset PlayerInput { get; private set; }

    public Action<InputAction.CallbackContext> OnCore1;
    public Action<InputAction.CallbackContext> OnCore2;
    public Action<InputAction.CallbackContext> OnCore3;
    public Action<InputAction.CallbackContext> OnWeaponBase;
    public Action<InputAction.CallbackContext> OnWeaponSpecial;

    public Action<InputAction.CallbackContext> OnMovement;

    public Action<InputAction.CallbackContext> OnLook;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already an InputManager in the scene. There can only be one. Destroying this",
                this);
            Destroy(this);
        }

        PlayerInput = new PlayerInputActionAsset();

        PlayerInput.Controls.Movement.started += ctx => OnMovement?.Invoke(ctx);
        PlayerInput.Controls.Movement.performed += ctx => OnMovement?.Invoke(ctx);
        PlayerInput.Controls.Movement.canceled += ctx => OnMovement?.Invoke(ctx);

        PlayerInput.Controls.Look.started += ctx => OnLook?.Invoke(ctx);
        PlayerInput.Controls.Look.performed += ctx => OnLook?.Invoke(ctx);
        PlayerInput.Controls.Look.canceled += ctx => OnLook?.Invoke(ctx);

        // PlayerInput.Controls.Core1.started += ctx => OnCore1?.Invoke(ctx);
        PlayerInput.Controls.Core1.performed += ctx => OnCore1?.Invoke(ctx);
        // PlayerInput.Controls.Core1.canceled += ctx => OnCore1?.Invoke(ctx);

        // PlayerInput.Controls.Core2.started += ctx => OnCore2?.Invoke(ctx);
        PlayerInput.Controls.Core2.performed += ctx => OnCore2?.Invoke(ctx);
        // PlayerInput.Controls.Core2.canceled += ctx => OnCore2?.Invoke(ctx);

        // PlayerInput.Controls.Core3.started += ctx => OnCore3?.Invoke(ctx);
        PlayerInput.Controls.Core3.performed += ctx => OnCore3?.Invoke(ctx);
        // PlayerInput.Controls.Core3.canceled += ctx => OnCore3?.Invoke(ctx);

        // PlayerInput.Controls.WeaponBase.started += ctx => OnWeaponBase?.Invoke(ctx);
        PlayerInput.Controls.WeaponBase.performed += ctx => OnWeaponBase?.Invoke(ctx);
        // PlayerInput.Controls.WeaponBase.canceled += ctx => OnWeaponBase?.Invoke(ctx);

        // PlayerInput.Controls.WeaponSpecial.started += ctx => OnWeaponSpecial?.Invoke(ctx);
        PlayerInput.Controls.WeaponSpecial.performed += ctx => OnWeaponSpecial?.Invoke(ctx);
        // PlayerInput.Controls.WeaponSpecial.canceled += ctx => OnWeaponSpecial?.Invoke(ctx);

        PlayerInput.Enable();
    }
}
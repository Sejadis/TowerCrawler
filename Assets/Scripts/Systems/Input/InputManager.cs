﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //TODO expose keybinds directly
    public static InputManager Instance { get; private set; }
    public PlayerInputActionAsset PlayerInput { get; private set; } //TODO private?

    public Action<InputAction.CallbackContext> OnCore1;
    public Action<InputAction.CallbackContext> OnCore2;
    public Action<InputAction.CallbackContext> OnCore3;
    public Action<InputAction.CallbackContext> OnWeaponBase;
    public Action<InputAction.CallbackContext> OnWeaponSpecial;

    public Action<InputAction.CallbackContext> OnMovement;
    public Action<InputAction.CallbackContext> OnLook;
    public Action<InputAction.CallbackContext> OnJump;
    public Action<InputAction.CallbackContext> OnSprint;

    public Action<InputAction.CallbackContext> OnAbilityUI;

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

        Application.targetFrameRate = 60;
        PlayerInput = new PlayerInputActionAsset();

        HookUpControls();
        PlayerInput.Controls.Enable();

        HookUpAbilities();
        PlayerInput.Abilities.Enable();

        HookUpUI();
        PlayerInput.UI.Enable();
    }

    private void HookUpControls()
    {
        PlayerInput.Controls.Movement.started += ctx => OnMovement?.Invoke(ctx);
        PlayerInput.Controls.Movement.performed += ctx => OnMovement?.Invoke(ctx);
        PlayerInput.Controls.Movement.canceled += ctx => OnMovement?.Invoke(ctx);

        PlayerInput.Controls.Look.started += ctx => OnLook?.Invoke(ctx);
        PlayerInput.Controls.Look.performed += ctx => OnLook?.Invoke(ctx);
        PlayerInput.Controls.Look.canceled += ctx => OnLook?.Invoke(ctx);

        // PlayerInput.Controls.Jump.started += ctx => OnJump?.Invoke(ctx);
        PlayerInput.Controls.Jump.performed += ctx => OnJump?.Invoke(ctx);
        // PlayerInput.Controls.Jump.canceled += ctx => OnJump?.Invoke(ctx);

        // PlayerInput.Controls.Jump.started += ctx => OnJump?.Invoke(ctx);
        PlayerInput.Controls.Sprint.performed += ctx => OnSprint?.Invoke(ctx);
        PlayerInput.Controls.Sprint.canceled += ctx => OnSprint?.Invoke(ctx);
    }

    private void HookUpAbilities()
    {
        // PlayerInput.Abilities.Core1.started += ctx => OnCore1?.Invoke(ctx);
        PlayerInput.Abilities.Core1.performed += ctx => OnCore1?.Invoke(ctx);
        // PlayerInput.Abilities.Core1.canceled += ctx => OnCore1?.Invoke(ctx);

        // PlayerInput.Abilities.Core2.started += ctx => OnCore2?.Invoke(ctx);
        PlayerInput.Abilities.Core2.performed += ctx => OnCore2?.Invoke(ctx);
        // PlayerInput.Abilities.Core2.canceled += ctx => OnCore2?.Invoke(ctx);

        // PlayerInput.Abilities.Core3.started += ctx => OnCore3?.Invoke(ctx);
        PlayerInput.Abilities.Core3.performed += ctx => OnCore3?.Invoke(ctx);
        // PlayerInput.Abilities.Core3.canceled += ctx => OnCore3?.Invoke(ctx);

        // PlayerInput.Abilities.WeaponBase.started += ctx => OnWeaponBase?.Invoke(ctx);
        PlayerInput.Abilities.WeaponBase.performed += ctx => OnWeaponBase?.Invoke(ctx);
        // PlayerInput.Abilities.WeaponBase.canceled += ctx => OnWeaponBase?.Invoke(ctx);

        // PlayerInput.Abilities.WeaponSpecial.started += ctx => OnWeaponSpecial?.Invoke(ctx);
        PlayerInput.Abilities.WeaponSpecial.performed += ctx => OnWeaponSpecial?.Invoke(ctx);
        // PlayerInput.Abilities.WeaponSpecial.canceled += ctx => OnWeaponSpecial?.Invoke(ctx);
    }

    private void HookUpUI()
    {
        // PlayerInput.UI.Upgrade.started += ctx => OnUpgradeUI?.Invoke(ctx);
        PlayerInput.UI.Ability.performed += ctx => OnAbilityUI?.Invoke(ctx);
        // PlayerInput.UI.Upgrade.canceled += ctx => OnUpgradeUI?.Invoke(ctx);
    }
}
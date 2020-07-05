using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerInputActionAsset PlayerInput { get; private set; }

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
        PlayerInput.Enable();
    }
}
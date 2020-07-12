using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform camera;
        [SerializeField] private float horizontalLookSensitivity = 0.25f;
        [SerializeField] private float verticalLookSensitivity = 0.25f;
        [SerializeField] private float lowerRotationLimit = -90;
        [SerializeField] private float upperRotationLimit = 90;
        private Vector2 lookData;
        private float currentCameraRotation;

        private void Start()
        {
            InputManager.Instance.OnLook += OnLook;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, lookData.y * horizontalLookSensitivity * Time.deltaTime);

            var rotation = lookData.x * verticalLookSensitivity * Time.deltaTime;
            currentCameraRotation += rotation;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, lowerRotationLimit, upperRotationLimit);
            camera.localRotation = Quaternion.Euler(currentCameraRotation, 0, 0);
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            lookData = new Vector2(-input.y, input.x).normalized;
        }
    }
}
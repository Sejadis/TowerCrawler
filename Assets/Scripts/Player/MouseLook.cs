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

        private void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnLook += OnLook;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnLook -= OnLook;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, lookData.y * horizontalLookSensitivity);
        }

        private void LateUpdate()
        {
            var rotation = lookData.x * verticalLookSensitivity;
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
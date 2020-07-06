using System;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HealthManager))]
    [RequireComponent(typeof(StatsManager))]
    public class PlayerController : MonoBehaviour, IEntityController
    {
        Rigidbody rigidBody;

        public Vector3 MovementData { get; private set; }
        public HealthManager HealthManager { get; private set; }

        public StatsManager StatsManager { get; private set; }
        [SerializeField] private Transform playerCamera;

        //
        // [SerializeField] private float moveSpeed = 20f;
        private float moveSpeed;

        [SerializeField] private float horizontalLookSensitivity = 0.5f;
        [SerializeField] private float verticalLookSensitivity = 10f;
        public Vector3 LookData { get; private set; }

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            HealthManager = GetComponent<HealthManager>();
            StatsManager = GetComponent<StatsManager>();
            Stat moveSpeedStat = StatsManager.GetStatByType(StatType.MovementSpeed);
            moveSpeedStat.OnStatChanged += (_, args) => moveSpeed = args.NewValue;
            moveSpeed = moveSpeedStat.Value;
        }

        private void Start()
        {
            InputManager.Instance.PlayerInput.Controls.Movement.started += OnMovement;
            InputManager.Instance.PlayerInput.Controls.Movement.performed += OnMovement;
            InputManager.Instance.PlayerInput.Controls.Movement.canceled += OnMovement;

            InputManager.Instance.PlayerInput.Controls.Look.started += OnLook;
            InputManager.Instance.PlayerInput.Controls.Look.performed += OnLook;
            InputManager.Instance.PlayerInput.Controls.Look.canceled += OnLook;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void FixedUpdate()
        {
            rigidBody.MoveRotation(rigidBody.rotation *
                                   Quaternion.Euler(new Vector3(0, LookData.y, 0) * horizontalLookSensitivity));
            playerCamera.Rotate(Vector3.right * (LookData.x * verticalLookSensitivity));
            Vector3 adjustedPosition = transform.forward * MovementData.z;
            adjustedPosition += transform.right * MovementData.x;
            rigidBody.MovePosition(transform.position + adjustedPosition * moveSpeed);
            LookData = Vector3.zero;
            // MovementData = Vector3.zero;
        }

        private void LateUpdate()
        {
            // playerCamera.Rotate(Vector3.right * (LookData.x * verticalLookSensitivity * Time.deltaTime));
        }

        public void OnMovement(InputValue value)
        {
            var input = value.Get<Vector2>();
            // var input = context.ReadValue<Vector2>();
            MovementData = new Vector3(input.x, 0, input.y).normalized;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            // var input = value.Get<Vector2>();
            var input = context.ReadValue<Vector2>();
            MovementData = new Vector3(input.x, 0, input.y).normalized;
        }

        public void OnLook(InputAction.CallbackContext context) //InputValue value)
        {
            // Debug.Log("onlook called");
            // LookData = value.Get<Vector2>();
            // var input = value.Get<Vector2>();
            var input = context.ReadValue<Vector2>();
            LookData += new Vector3(-input.y, input.x, 0);
        }
    }
}
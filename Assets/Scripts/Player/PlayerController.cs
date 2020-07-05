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

        //
        // [SerializeField] private float moveSpeed = 20f;
        private float moveSpeed;

        [SerializeField] private float lookSensitivity = 20;
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
        }

        void FixedUpdate()
        {
            // float speed = moveSpeed * moveSpeedStat.Value;
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(LookData * lookSensitivity));
            Vector3 newPos = transform.forward * MovementData.z;
            newPos += transform.right * MovementData.x;
            rigidBody.MovePosition(transform.position + newPos * moveSpeed);
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

        public void OnLook(InputAction.CallbackContext context)//InputValue value)
        {
            Debug.Log("onlook called");
            // LookData = value.Get<Vector2>();
            // var input = value.Get<Vector2>();
            var input = context.ReadValue<Vector2>();
            LookData = new Vector3(0, input.x, 0);
        }
    }
}
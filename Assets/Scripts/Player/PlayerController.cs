using System;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IEntityController
    {
        public Rigidbody RigidBody { get; private set; }

        public Vector3 MovementData { get; private set; }
        public HealthManager HealthManager { get; private set; }

        public StatsManager StatsManager { get; private set; }
        [SerializeField] private Transform playerCamera;
        private float currentCameraRotation;
        public float lowerRoationLimit;
        public float upperRoationLimit;

        //
        // [SerializeField] private float moveSpeed = 20f;
        private float moveSpeed;

        [SerializeField] private float horizontalLookSensitivity = 0.5f;
        [SerializeField] private float verticalLookSensitivity = 10f;
        private bool shouldMove;
        private Vector3 warpDirection;
        public Vector3 LookData { get; private set; }

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Stat moveSpeedStat = GetComponent<StatsManager>()?.GetStatByType(StatType.MovementSpeed);
            if (moveSpeedStat != null)
            {
                moveSpeedStat.OnStatChanged += (_, args) => moveSpeed = args.NewValue;
                moveSpeed = moveSpeedStat.Value;
            }

            currentCameraRotation = playerCamera.rotation.eulerAngles.x;
        }

        private void Start()
        {
            InputManager.Instance.OnMovement += OnMovement;

            InputManager.Instance.OnLook += OnLook;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void FixedUpdate()
        {
            RigidBody.MoveRotation(RigidBody.rotation *
                                   Quaternion.Euler(new Vector3(0, LookData.y, 0) * horizontalLookSensitivity));
            var desiredRotation = currentCameraRotation + LookData.x * verticalLookSensitivity;
            if (desiredRotation < upperRoationLimit && desiredRotation > lowerRoationLimit)
            {
                playerCamera.Rotate(Vector3.right * (LookData.x * verticalLookSensitivity));
                currentCameraRotation = desiredRotation;
            }

            Vector3 adjustedPosition = transform.forward * MovementData.z;
            adjustedPosition += transform.right * MovementData.x;
            if (shouldMove)
            {
                adjustedPosition += warpDirection;
                // adjustedPosition += (rigidBody.velocity != Vector3.zero ? rigidBody.velocity.normalized : rigidBody.transform.forward) * 100;
                shouldMove = false;
            }

            RigidBody.MovePosition(transform.position + adjustedPosition * moveSpeed);


            LookData = Vector3.zero;
            // MovementData = Vector3.zero;
        }

        private void LateUpdate()
        {
            // playerCamera.Rotate(Vector3.right * (LookData.x * verticalLookSensitivity * Time.deltaTime));
        }

        public void WarpPosition(Vector3 warpDirection)
        {
            shouldMove = true;
            this.warpDirection = warpDirection;
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
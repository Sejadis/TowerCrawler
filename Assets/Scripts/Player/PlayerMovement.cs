using System;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, IEntityController
    {
        public bool IsMoving { get; private set; }
        public event EventHandler<bool> OnMoveStateChanged;

        [SerializeField] private CharacterController controller;

        [SerializeField] private float moveSpeed = 15f;

        [SerializeField] private float gravityMultiplier = 1;

        [SerializeField] private float jumpHeight = 5;

        [SerializeField] private float groundCheckRadius = 0.05f;

        private Vector2 movementData;

        private Vector3 yVelocity = Vector3.zero;
        public Vector3 FrameVelocity { get; private set; }

        private bool isGrounded;

        [SerializeField] private int allowedMidAirJumps = 0; //TODO remove serializeField

        private int performedMidAirJumps;

        private void Awake()
        {
            StatsManager statsManager = GetComponent<StatsManager>();
            if (statsManager != null)
            {
                Stat moveSpeedStat = statsManager.GetStatByType(StatType.MovementSpeed);
                if (moveSpeedStat != null)
                {
                    moveSpeedStat.OnStatChanged += (_, args) => moveSpeed = args.NewValue;
                    moveSpeed = moveSpeedStat.Value;
                }

                Stat midAirJumpStat = statsManager.GetStatByType(StatType.MidAirJump);
                if (midAirJumpStat != null)
                {
                    midAirJumpStat.OnStatChanged += (_, args) => allowedMidAirJumps = (int) args.NewValue;
                    allowedMidAirJumps = (int) midAirJumpStat.Value;
                }
            }
        }

        private void Start()
        {
            InputManager.Instance.OnMovement += OnMovement;
            InputManager.Instance.OnJump += OnJump;
        }

        private void Update()
        {
            var lastMoveState = IsMoving;
            var lastPosition = transform.position;
            GroundCheck();
            if (isGrounded)
            {
                performedMidAirJumps = 0;
            }

            ApplyInputMovement();
            ApplyGravity();
            FrameVelocity = transform.position - lastPosition;
            IsMoving = !(FrameVelocity == Vector3.zero);
            if (IsMoving != lastMoveState)
            {
                OnMoveStateChanged?.Invoke(this, IsMoving);
            }
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            movementData = new Vector2(input.x, input.y).normalized;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (isGrounded || allowedMidAirJumps > performedMidAirJumps)
            {
                yVelocity.y = jumpHeight;
                if (!isGrounded)
                {
                    performedMidAirJumps++;
                }
            }
        }

        private void ApplyInputMovement()
        {
            var finalMove = movementData.x * transform.right;
            finalMove += movementData.y * transform.forward;
            finalMove.Normalize();
            finalMove *= moveSpeed * Time.deltaTime;
            controller.Move(finalMove);
        }

        private void ApplyGravity()
        {
            if (isGrounded && yVelocity.y < 0)
            {
                yVelocity.y = -2f;
            }
            else
            {
                yVelocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
            }

            controller.Move(yVelocity * Time.deltaTime);
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, controller.height / 2f, 0),
                groundCheckRadius,
                1 << LayerMask.NameToLayer("Ground"));
        }

        public void WarpPosition(Vector3 direction)
        {
            controller.Move(direction);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position - new Vector3(0, controller.height / 2f, 0), groundCheckRadius);
        }
    }
}
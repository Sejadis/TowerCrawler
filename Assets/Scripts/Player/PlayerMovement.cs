using System;
using SejDev.Systems.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float gravityMultiplier = 1;
        [SerializeField] private float jumpHeight = 5;
        [SerializeField] private float groundCheckRadius = 0.05f;
        private Vector2 movementData;
        private Vector3 velocity = Vector3.zero;
        private bool isGrounded;
        private bool isMoving;
        [SerializeField] private int allowedMidAirJumps = 0;
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
            var lastMoveState = isMoving;
            var lastPosition = transform.position;
            GroundCheck();
            if (isGrounded)
            {
                performedMidAirJumps = 0;
            }

            Move();
            ApplyGravity();
            isMoving = lastPosition != transform.position;
            if (isMoving != lastMoveState)
            {
                //trigger movestate chan
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
                velocity.y = jumpHeight;
                if (!isGrounded)
                {
                    performedMidAirJumps++;
                }
            }
        }

        private void Move()
        {
            var finalMove = movementData.x * transform.right;
            finalMove += movementData.y * transform.forward;
            finalMove.Normalize();
            finalMove *= moveSpeed * Time.deltaTime;
            controller.Move(finalMove);
        }

        private void ApplyGravity()
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            else
            {
                velocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
            }

            controller.Move(velocity * Time.deltaTime);
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, controller.height / 2f, 0),
                groundCheckRadius,
                1 << LayerMask.NameToLayer("Ground"));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position - new Vector3(0, controller.height / 2f, 0), groundCheckRadius);
        }
    }
}
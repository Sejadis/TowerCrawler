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
        private Vector2 movementData;

        private void Awake()
        {
            Stat moveSpeedStat = GetComponent<StatsManager>()?.GetStatByType(StatType.MovementSpeed);
            if (moveSpeedStat != null)
            {
                moveSpeedStat.OnStatChanged += (_, args) => moveSpeed = args.NewValue;
                moveSpeed = moveSpeedStat.Value;
            }
        }

        private void Start()
        {
            InputManager.Instance.OnMovement += OnMovement;
        }

        private void Update()
        {
            var finalMove = movementData.x * transform.right;
            finalMove += movementData.y * transform.forward;
            finalMove *= moveSpeed * Time.deltaTime;
            finalMove += Physics.gravity * gravityMultiplier;
            controller.Move(finalMove);
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            movementData = new Vector2(input.x, input.y).normalized;
        }
    }
}
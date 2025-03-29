using UnityEngine;
using UnityEngine.InputSystem; // Required for new Input System

namespace Dig_Simulator
{
    public class XRMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.5f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private float flySpeed = 2.0f; // Speed while flying

        private Vector3 velocity;
        private bool isGrounded;
        private bool isFlying = false; // Toggle for Fly Mode
        private XRIDefaultInputActions inputSystem;

        private void Start()
        {
            inputSystem = new XRIDefaultInputActions();
            inputSystem.XRIRightLocomotion.Enable();
        }

        private void Update()
        {
            HandleMovement();

            if (isFlying)
            {
                HandleFlying();
            }
            else
            {
                HandleJumping();
            }

            ToggleFlyMode();
        }

        private void HandleMovement()
        {
            Vector2 moveInput = inputSystem.XRIRightLocomotion.Move.ReadValue<Vector2>(); // Read input
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            move = transform.TransformDirection(move); // Convert local to world space
            characterController.Move(move * (moveSpeed * Time.deltaTime));
        }

        private void HandleJumping()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Reset falling velocity
            }

            if (isGrounded && inputSystem.XRIRightLocomotion.Jump.WasPressedThisFrame())
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        private void HandleFlying()
        {
            float verticalInput = inputSystem.XRIRightLocomotion.Fly.ReadValue<float>(); // Read Fly input (-1 = Down, 1 = Up)
            Vector3 flyMovement = new Vector3(0, verticalInput * flySpeed, 0);
            characterController.Move(flyMovement * Time.deltaTime);
        }

        private void ToggleFlyMode()
        {
            if (inputSystem.XRIRightLocomotion.Fly.WasPressedThisFrame())
            {
                isFlying = !isFlying;
                velocity = Vector3.zero; // Reset velocity when toggling mode
            }
        }
    }
}

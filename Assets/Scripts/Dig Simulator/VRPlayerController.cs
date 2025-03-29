using UnityEngine;
using UnityEngine.InputSystem;

namespace Dig_Simulator
{
    public class VRPlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpHeight = 2f;
        private CharacterController characterController;

        [SerializeField] private InputActionProperty moveAction;
        [SerializeField] private InputActionProperty jumpAction;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;

            if (jumpAction.action.triggered && characterController.isGrounded)
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    private CharacterController controller;
    private PlayerInput playerInput;

    private Vector3 velocity;
    private Vector3 smoothV;
    private float verticalVelocity;
    
    private bool jumping;
    private float lastGroundedTime;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.OnJumpEvent += HandleJump;
    }

    private void OnDisable()
    {
        playerInput.OnJumpEvent -= HandleJump;
    }

    private void Update()
    {
        CalculateMovement();
        ApplyGravityAndMove();
    }

    private void CalculateMovement()
    {
        Vector3 inputDir = new Vector3(playerInput.MoveInput.x, 0, playerInput.MoveInput.y).normalized;
        Vector3 worldInputDir = transform.TransformDirection(inputDir);

        float currentSpeed = playerInput.IsSprinting ? settings.runSpeed : settings.walkSpeed;
        Vector3 targetVelocity = worldInputDir * currentSpeed;
        
        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothV, settings.smoothMoveTime);
    }

    private void ApplyGravityAndMove()
    {
        verticalVelocity -= settings.gravity * Time.deltaTime;
        velocity = new Vector3(velocity.x, verticalVelocity, velocity.z);

        var flags = controller.Move(velocity * Time.deltaTime);
        
        if (flags == CollisionFlags.Below)
        {
            jumping = false;
            lastGroundedTime = Time.time;
            verticalVelocity = 0;
        }
    }

    private void HandleJump()
    {
        float timeSinceLastTouchedGround = Time.time - lastGroundedTime;
        
        if (controller.isGrounded || (!jumping && timeSinceLastTouchedGround < 0.15f))
        {
            jumping = true;
            verticalVelocity = settings.jumpForce;
        }
    }
}
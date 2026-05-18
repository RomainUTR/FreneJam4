using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    public RSE_OnPlayerDeath OnPlayerDeath;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Camera _mainCamera;

    private Vector3 velocity;
    private Vector3 smoothV;
    private float verticalVelocity;

    void OnEnable()
    {
        OnPlayerDeath.OnEventRaised += DisablePlayer;
    }

    void OnDisable()
    {
        OnPlayerDeath.OnEventRaised -= DisablePlayer;      
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CalculateMovement();
        AimAtMouse();

        controller.Move(velocity * Time.deltaTime);
    }

    private void CalculateMovement()
    {
        Vector3 inputDir = new Vector3(playerInput.MoveInput.x, 0, playerInput.MoveInput.y).normalized;

        float currentSpeed = playerInput.IsSprinting ? settings.runSpeed : settings.walkSpeed;

        Vector3 targetVelocity = inputDir * currentSpeed;

        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothV, settings.smoothMoveTime);
    }

    void AimAtMouse()
    {
        Ray ray = _mainCamera.ScreenPointToRay(playerInput.LookInput);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            Vector3 lookTarget = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(lookTarget);
        }
    }

    void DisablePlayer()
    {
        enabled = false;
    }
}
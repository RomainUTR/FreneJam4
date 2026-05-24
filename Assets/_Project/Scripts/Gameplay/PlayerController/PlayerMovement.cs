using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Title("Data")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public RSO_PlayerRuntimeStats runtimeStats;

    [Title("Malus & Events")]
    public RSE_OnPlayerDeath OnPlayerDeath;
    public RSF_ForceReduceSpeedMovement ForceReduceSpeedMovement;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Camera _mainCamera;

    private Vector3 velocity;
    private Vector3 smoothV;
    private float verticalVelocity;

    private float speedModifier = 1f;
    private float reduceSpeedDuration = 5f;

    void OnEnable()
    {
        OnPlayerDeath.OnEventRaised += DisablePlayer;
        ForceReduceSpeedMovement.OnInvoke = HandleReduceSpeed;
    }

    void OnDisable()
    {
        OnPlayerDeath.OnEventRaised -= DisablePlayer;
        ForceReduceSpeedMovement.OnInvoke = null;
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

        float currentSpeed = playerInput.IsSprinting
            ? runtimeStats.currentRunSpeed
            : runtimeStats.basePlayerSettings.walkSpeed;

        float modifiedSpeed = currentSpeed * speedModifier;

        Vector3 targetVelocity = inputDir * modifiedSpeed;

        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothV, runtimeStats.basePlayerSettings.smoothMoveTime);
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

    bool HandleReduceSpeed()
    {
        if (speedModifier == 0.5f) return false;

        speedModifier = 0.5f;
        StartCoroutine(WaitForReturnToNormalSpeed());

        return true;
    }

    IEnumerator WaitForReturnToNormalSpeed()
    {
        yield return new WaitForSeconds(reduceSpeedDuration);
        speedModifier = 1f;
    }
}
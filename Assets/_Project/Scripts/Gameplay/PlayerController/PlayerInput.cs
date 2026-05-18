using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions ctx;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }

    public event Action OnJumpEvent, OnInteractEvent;

    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    void Awake()
    {
        ctx = new InputSystem_Actions();
        RebindStorage.Load(ctx.asset);
    }

    void OnEnable() => ctx.Enable();
    void OnDisable() => ctx.Disable();

    private void Start()
    {
        if (settings.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        MoveInput = ctx.Controller.Move.ReadValue<Vector2>();
        LookInput = ctx.Controller.Look.ReadValue<Vector2>();
        IsSprinting = ctx.Controller.Sprint.IsPressed();

        if (ctx.Controller.Jump.WasPerformedThisFrame())
        {
            OnJumpEvent?.Invoke();
        }

        if (ctx.Controller.Interact.WasPressedThisFrame())
        {
            OnInteractEvent?.Invoke();
        }
    }
}

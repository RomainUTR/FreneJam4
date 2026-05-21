using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions ctx;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsShooting { get; private set; }

    public event Action OnJumpEvent, OnInteractEvent;

    private Vector2 _inputMultiplier = new Vector2(1f, 1f);

    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    public RSE_ToggleInputInversion ToggleInputInversion;
    public RSF_ForceSwapInput ForceSwapInput;

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _sprintAction;
    private InputAction _shootAction;
    private InputAction _interactAction;

    private bool _isInputInverted = false;
    public float SwapInputDuration = 5f;

    void Awake()
    {
        ctx = new InputSystem_Actions();
        RebindStorage.Load(ctx.asset);
    }

    void OnEnable()
    {
        ctx.Enable();
        ToggleInputInversion.OnEventRaised += SwapInput;
        ForceSwapInput.OnInvoke = HandleSwapInput;
    }
    void OnDisable()
    {
        ctx.Disable();
        ToggleInputInversion.OnEventRaised -= SwapInput;
        ForceSwapInput.OnInvoke = HandleSwapInput;
    }

    private void Start()
    {
        if (settings.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        SetInputMap(false);
    }

    void Update()
    {
        MoveInput = _moveAction.ReadValue<Vector2>() * _inputMultiplier;
        LookInput = _lookAction.ReadValue<Vector2>();
        IsSprinting = _sprintAction.IsPressed();

        IsShooting = _shootAction.IsPressed();

        if (ctx.Controller.Interact.WasPressedThisFrame())
        {
            OnInteractEvent?.Invoke();
        }
    }

    private void SwapInput()
    {
        if (_inputMultiplier.x == 1f && _inputMultiplier.y == 1f)
        {
            _inputMultiplier = new Vector2(-1f, -1f);
        } else if (_inputMultiplier.x == -1f && _inputMultiplier.y == -1f)
        {
            _inputMultiplier = new Vector2(1f, 1f);
        }

        if (_isInputInverted == true)
        {
            SetInputMap(false);
        } else
        {
            SetInputMap(true);
        }
    }

    public void SetInputMap(bool isInverted)
    {
        if (_currentMap != null)
        {
            _currentMap.Disable();
        }

        _isInputInverted = isInverted;

        string mapName = isInverted ? "InvertedController" : "Controller";
        _currentMap = ctx.asset.FindActionMap(mapName);

        _moveAction = _currentMap.FindAction("Move");
        _lookAction = _currentMap.FindAction("Look");
        _sprintAction = _currentMap.FindAction("Sprint");
        _shootAction = _currentMap.FindAction("Shoot");
        _interactAction = _currentMap.FindAction("Interact");

        _currentMap.Enable();
    }

    private bool HandleSwapInput()
    {
        if (_isInputInverted == true) return false;

        SwapInput();
        StartCoroutine(WaitForReturnToNormalInput());

        return true;
    }

    IEnumerator WaitForReturnToNormalInput()
    {
        yield return new WaitForSeconds(SwapInputDuration);

        SwapInput();
    }
}

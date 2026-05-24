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

    public event Action OnInputMapChanged;

    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    [Title("Malus Settings")]
    public RSF_ForceSwapInput ForceSwapInput;
    public float SwapInputDuration = 5f;

    [InfoBox("Les noms exacts de des Action Maps dans l'InputSystem")]
    public string[] ShuffledMapNames = { "Shuffle1", "Shuffle2", "Shuffle3" };
    private readonly string _baseMapName = "Controller";

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _sprintAction;
    private InputAction _shootAction;
    private InputAction _interactAction;

    private bool _isShuffled = false;

    void Awake()
    {
        ctx = new InputSystem_Actions();
        RebindStorage.Load(ctx.asset);
    }

    void OnEnable()
    {
        ctx.Enable();
        ForceSwapInput.OnInvoke = HandleSwapInput;
    }

    void OnDisable()
    {
        ctx.Disable();
        ForceSwapInput.OnInvoke = null;
    }

    private void Start()
    {
        if (settings.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        SetInputMap(_baseMapName);
    }

    void Update()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        LookInput = _lookAction.ReadValue<Vector2>();
        IsSprinting = _sprintAction.IsPressed();
        IsShooting = _shootAction.IsPressed();

        if (_interactAction.WasPressedThisFrame())
        {
            OnInteractEvent?.Invoke();
        }
    }

    public void SetInputMap(string mapName)
    {
        if (_currentMap != null)
        {
            _currentMap.Disable();
        }

        _currentMap = ctx.asset.FindActionMap(mapName);

        _moveAction = _currentMap.FindAction("Move");
        _lookAction = _currentMap.FindAction("Look");
        _sprintAction = _currentMap.FindAction("Sprint");
        _shootAction = _currentMap.FindAction("Shoot");
        _interactAction = _currentMap.FindAction("Interact");

        _currentMap.Enable();

        OnInputMapChanged?.Invoke();
    }

    private bool HandleSwapInput()
    {
        if (_isShuffled || ShuffledMapNames.Length == 0) return false;

        _isShuffled = true;

        int randomIndex = UnityEngine.Random.Range(0, ShuffledMapNames.Length);
        string randomMapName = ShuffledMapNames[randomIndex];

        SetInputMap(randomMapName);
        StartCoroutine(WaitForReturnToNormalInput());

        return true;
    }

    IEnumerator WaitForReturnToNormalInput()
    {
        yield return new WaitForSeconds(SwapInputDuration);

        _isShuffled = false;
        SetInputMap(_baseMapName);
    }

    public string GetActionKeyName(string actionName, string expectedPart = "")
    {
        if (_currentMap == null) return "";

        InputAction action = _currentMap.FindAction(actionName);
        if (action != null)
        {
            if (string.IsNullOrEmpty(expectedPart))
            {
                return action.GetBindingDisplayString(0);
            }

            for (int i = 0; i < action.bindings.Count; i++)
            {
                InputBinding binding = action.bindings[i];

                if (binding.isPartOfComposite && binding.name.ToLower() == expectedPart.ToLower())
                {
                    string displayString = action.GetBindingDisplayString(i);

                    if (!string.IsNullOrEmpty(displayString))
                    {
                        return displayString;
                    }
                }
            }
        }

        return "";
    }
}
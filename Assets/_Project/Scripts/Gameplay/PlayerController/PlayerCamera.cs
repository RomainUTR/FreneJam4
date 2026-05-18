using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCamera : MonoBehaviour
{
    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required]
    public PlayerSettingsSO settings;

    [Title("References")]
    [Required]
    public Camera cam;

    private PlayerInput playerInput;

    private float yaw;
    private float pitch;
    private float smoothYaw;
    private float smoothPitch;
    private float yawSmoothV;
    private float pitchSmoothV;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        if (cam == null) cam = Camera.main;

        if (pitch > 180f)
        {
            pitch -= 360f;
        }

        yaw = transform.eulerAngles.y;
        pitch = cam.transform.localEulerAngles.x;
        smoothYaw = yaw;
        smoothPitch = pitch;
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        Vector2 mouseDelta = playerInput.LookInput;

        float mX = mouseDelta.x;
        float mY = mouseDelta.y;

        yaw += mX * settings.mouseSensitivity;
        pitch -= mY * settings.mouseSensitivity;

        pitch = Mathf.Clamp(pitch, settings.pitchMinMax.x, settings.pitchMinMax.y);

        smoothPitch = Mathf.SmoothDampAngle(smoothPitch, pitch, ref pitchSmoothV, settings.rotationSmoothTime);
        smoothYaw = Mathf.SmoothDampAngle(smoothYaw, yaw, ref yawSmoothV, settings.rotationSmoothTime);

        transform.eulerAngles = Vector3.up * smoothYaw;

        cam.transform.localEulerAngles = Vector3.right * smoothPitch;
    }
}
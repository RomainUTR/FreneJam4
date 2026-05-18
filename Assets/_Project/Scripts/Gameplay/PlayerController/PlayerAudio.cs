using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerAudio : MonoBehaviour
{
    [Title("Configuration")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [Required, SerializeField]
    private PlayerSettingsSO settings;
    [SerializeField, Required] private RSE_PlaySFXSound PlaySFXSound;

    private CharacterController controller;
    private PlayerInput playerInput;

    private float stepTimer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        if (controller.isGrounded && playerInput.MoveInput.sqrMagnitude > 0.01f)
        {
            float currentInterval = playerInput.IsSprinting ? settings.runStepInterval : settings.walkStepInterval;

            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstepSound();
                stepTimer = currentInterval;
            }
        }
        else
        {
            stepTimer = 0;
        }
    }

    private void PlayFootstepSound()
    {
        if (settings.soundData != null)
        {
            PlaySFXSound?.RaiseEvent(settings.soundData, transform.position);
        }
    }
}
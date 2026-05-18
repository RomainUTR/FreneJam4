using System.Collections;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image HealthBarImage;
    [InlineEditor] public PlayerSettingsSO Settings;
    [Required] public RSE_OnHealthChanged OnHealthChanged;
    
    [Header("Flash Settings")]
    public float FlashDuration = 0.2f;
    public MMF_Player Feedbacks;

    private Material _healthMaterial;
    private float _previousHealth;
    private Coroutine _flashCoroutine;

    void Start()
    {
        HealthBarImage.material = new Material(HealthBarImage.material);
        _healthMaterial = HealthBarImage.material;
        
        _previousHealth = Settings.maxHealth;
        UpdateHealthDisplay(Settings.maxHealth);
    }

    void OnEnable() 
    { 
        OnHealthChanged.OnEventRaised += UpdateHealthDisplay; 
    }

    void OnDisable() 
    { 
        OnHealthChanged.OnEventRaised -= UpdateHealthDisplay; 
    }

    void OnDestroy() 
    { 
        if (_healthMaterial != null) Destroy(_healthMaterial); 
    }

    public void UpdateHealthDisplay(float currentHealth)
    {
        float fillAmount = (float)currentHealth / Settings.maxHealth;
        _healthMaterial.SetFloat("_Fill", fillAmount);
        Feedbacks.PlayFeedbacks();

        if (currentHealth < _previousHealth)
        {
            if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
            _flashCoroutine = StartCoroutine(FlashRoutine());
        }

        _previousHealth = currentHealth;
    }

    private IEnumerator FlashRoutine()
    {
        float elapsedTime = 0f;
        _healthMaterial.SetFloat("_FlashAmount", 1f);

        while (elapsedTime < FlashDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentFlash = Mathf.Lerp(1f, 0f, elapsedTime / FlashDuration);
            _healthMaterial.SetFloat("_FlashAmount", currentFlash);
            
            yield return null;
        }

        _healthMaterial.SetFloat("_FlashAmount", 0f);
    }
}
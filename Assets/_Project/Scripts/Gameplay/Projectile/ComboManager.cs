using Sirenix.OdinInspector;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [Required] public RSE_OnComboFinished OnComboFinished;
    [Required] public RSE_OnHealPlayer OnHealPlayer;
    public int ComboCap = 7;
    public int HealValue = 1;

    private void OnEnable()
    {
        OnComboFinished.OnEventRaised += HandleComboEffect;
    }

    private void OnDisable()
    {
        OnComboFinished.OnEventRaised -= HandleComboEffect;
    }

    private void HandleComboEffect(int amount)
    {
        if (amount >= ComboCap)
        {
            Debug.Log("Combo");
            OnHealPlayer.RaiseEvent(HealValue);
        }
    }
}

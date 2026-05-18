using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_EnemyTakeDamage", menuName = "Events/RSE_EnemyTakeDamage")]
public class RSE_EnemyTakeDamage : ScriptableObject
{
    public event Action<GameObject, float> OnEventRaised;

    public void RaiseEvent(GameObject GO, float value)
    {
        OnEventRaised?.Invoke(GO, value);
    }
}
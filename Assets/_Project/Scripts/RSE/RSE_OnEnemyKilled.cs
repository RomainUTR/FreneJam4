using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnEnemyKilled", menuName = "Scriptable Objects/RSE_OnEnemyKilled")]
public class RSE_OnEnemyKilled : ScriptableObject
{
    public event Action<GameObject> OnEventRaised;

    public void RaiseEvent(GameObject GO)
    {
        OnEventRaised.Invoke(GO);
    }
}

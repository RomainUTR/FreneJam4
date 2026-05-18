using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnEnemyKilled", menuName = "Scriptable Objects/RSE_OnEnemyKilled")]
public class RSE_OnEnemyKilled : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}

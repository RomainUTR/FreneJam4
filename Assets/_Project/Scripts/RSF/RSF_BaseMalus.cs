using System;
using UnityEngine;

public abstract class RSF_BaseMalus : ScriptableObject
{
    public Func<bool> OnInvoke;

    public bool Execute()
    {
        if (OnInvoke != null)
        {
            return OnInvoke.Invoke();
        }

        Debug.LogWarning($"Personne n'est abonné au RSF {this.name}");
        return false;
    }
}

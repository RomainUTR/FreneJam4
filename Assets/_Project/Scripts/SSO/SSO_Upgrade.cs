using UnityEngine;

public abstract class SSO_Upgrade : ScriptableObject
{
    [Header("UI")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public abstract void ApplyUpgrade();
}

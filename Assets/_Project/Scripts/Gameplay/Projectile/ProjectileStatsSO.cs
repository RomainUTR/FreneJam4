using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStatsSO", menuName = "Scriptable Objects/ProjectileStatsSO")]
public class ProjectileStatsSO : ScriptableObject
{
    [Header("Movement")]
    [Range(0f,50f)] public float Speed = 10f;
    [Range(0f,1f)] public float CollisionRadius = 0.5f;

    [Header("Combat")]
    public int damage = 1;
    public float PlayerGracePeriod = 0.5f;

    [Header("Physics Logic")]
    public LayerMask BounceMask;

    public int MaxBounces = 100;
}

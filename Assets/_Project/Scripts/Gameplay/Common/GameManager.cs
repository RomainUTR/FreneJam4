using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField, InlineEditor] private RSO_PlayerRuntimeStats PlayerRuntimeStats;
    [SerializeField, InlineEditor] private RSO_EnemyScaling EnemyScaling;

    //[Header("Input")]

    //[Header("Output")]


    private void Awake()
    {
        if (PlayerRuntimeStats != null)
        {
            PlayerRuntimeStats.Initialize();
        }

        if (EnemyScaling != null)
        {
            EnemyScaling.Initialize();
        }
    }
}
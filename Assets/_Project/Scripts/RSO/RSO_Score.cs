using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "RSO_Score", menuName = "Data/RSO/RSO_Score")]
public class RSO_Score : ScriptableObject
{
    [Header("Runtime Value")]
    public float initialScore;
    
    //[HideInInspector]
    public float RuntimeValue;

    private void OnEnable()
    {
        // Resets the value when the game starts
        RuntimeValue = initialScore;
    }

    public void ResetValue()
    {
        RuntimeValue = initialScore;
    }
}
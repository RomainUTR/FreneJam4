using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public RSE_OnEnemyKilled OnEnemyKilled;
    public RSO_Score Score;
    void OnEnable()
    {
        OnEnemyKilled.OnEventRaised += AddScore;
    }

    void OnDisable()
    {
        OnEnemyKilled.OnEventRaised -= AddScore;
    }

    void Awake()
    {
        Score.ResetValue();
        UpdateUI();
    }

    void AddScore(GameObject GO)
    {
        Score.RuntimeValue += 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        ScoreUI.text = Score.RuntimeValue.ToString();
    }
}

using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public RSE_OnEnemyKilled OnEnemyKilled;
    private int _currentScore = 0;

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
        _currentScore = 0;
        UpdateUI();
    }

    void AddScore()
    {
        _currentScore += 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        ScoreUI.text = _currentScore.ToString();
    }
}

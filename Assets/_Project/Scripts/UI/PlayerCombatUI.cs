using MoreMountains.Feedbacks;
using Shapes;
using UnityEngine;

public class PlayerCombatUI : MonoBehaviour
{
    public PlayerCombat CombatScript;
    public Disc CooldownArc;

    public float MaxAngleDegrees = 180f;

    public MMF_Player FeedbacksCooldown, FeedbacksShoot;

    private bool _isReady = true;
    private float _startAngleRad;

    void OnEnable()
    {
        CombatScript.OnPlayerShoot += HandleShootFeedback;
    }

    void OnDisable()
    {
        CombatScript.OnPlayerShoot -= HandleShootFeedback;
    }

    void Start()
    {
        _startAngleRad = CooldownArc.AngRadiansStart;
        HandleReadyFeedback(1f);
    }

    void Update()
    {
        float ratio = CombatScript.GetCooldownRatio();
        float maxAngleRad = MaxAngleDegrees * Mathf.Deg2Rad;
        CooldownArc.AngRadiansEnd = _startAngleRad + (maxAngleRad * ratio);

        HandleReadyFeedback(ratio);
    }

    void HandleReadyFeedback(float currentRatio)
    {
        if (currentRatio >= 1f && !_isReady)
        {
            _isReady = true;
            FeedbacksCooldown.PlayFeedbacks();
        } else if (currentRatio < 1f && _isReady)
        {
            _isReady = false;
            FeedbacksCooldown.StopFeedbacks();
            FeedbacksCooldown.RestoreInitialValues();
        }
    }

    void HandleShootFeedback()
    {
        FeedbacksShoot.PlayFeedbacks();
    }
}

using MoreMountains.Feedbacks;
using Shapes;
using UnityEngine;

public class PlayerCombatUI : MonoBehaviour
{
    public PlayerCombat CombatScript;
    public Disc HeatArc;

    public float MaxAngleDegrees = 180f;
    public Color ColdColor = Color.white;
    public Color HotColor = Color.red;

    public MMF_Player FeedbacksReady, FeedbacksShoot, FeedbacksOverheat;

    private bool _wasOverheated = false;
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
        _startAngleRad = HeatArc.AngRadiansStart;
    }

    void Update()
    {
        float maxAngleRad = MaxAngleDegrees * Mathf.Deg2Rad;
        HeatArc.AngRadiansEnd = _startAngleRad + (maxAngleRad * CombatScript.currentHeat);

        HeatArc.Color = Color.Lerp(ColdColor, HotColor, CombatScript.currentHeat);

        HandleHeatStateFeedbacks();
    }

    void HandleHeatStateFeedbacks()
    {
        if (CombatScript.isOverheated && !_wasOverheated)
        {
            _wasOverheated = true;
            if (FeedbacksOverheat != null) FeedbacksOverheat.PlayFeedbacks();
        }
        else if (!CombatScript.isOverheated && _wasOverheated)
        {
            _wasOverheated = false;
            if (FeedbacksReady != null) FeedbacksReady.PlayFeedbacks();
        }
    }

    void HandleShootFeedback()
    {
        FeedbacksShoot.PlayFeedbacks();
    }
}
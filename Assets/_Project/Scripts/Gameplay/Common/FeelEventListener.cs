using MoreMountains.Feedbacks;
using UnityEngine;

public class FeelEventListener : MonoBehaviour
{
    public RSE_OnDamakeTaken OnDamakeTaken;
    public RSE_OnHealPlayer OnHealPlayer;
    public MMF_Player FeedbackPlayer, Heal;

    void OnEnable()
    {
        if (OnDamakeTaken != null)
        {
            OnDamakeTaken.OnEventRaised += PlayFeedbacks;
        } 
        OnHealPlayer.OnEventRaised += HealFeedbacks;
    }

    void OnDisable()
    {
        if (OnDamakeTaken != null)
        {
            OnDamakeTaken.OnEventRaised -= PlayFeedbacks;
        } 
        OnHealPlayer.OnEventRaised -= HealFeedbacks;
    }

    void PlayFeedbacks(int amount)
    {
        if (FeedbackPlayer != null)
        {
            FeedbackPlayer.PlayFeedbacks();
        }
    }

    void HealFeedbacks(int amount)
    {
        if (Heal != null)
        {
            Heal.PlayFeedbacks();
        }
    }
}

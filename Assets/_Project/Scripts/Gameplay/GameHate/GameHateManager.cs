using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHateManager : MonoBehaviour
{
    [Title("Hate Deck")]
    [InfoBox("Liste de toutes les crasses que le jeu peut infliger au joueur.")]
    [Required, SerializeField] private List<RSF_BaseMalus> MalusPool = new List<RSF_BaseMalus>();

    public RSE_AskForPunishment AskForPunishment;

    [Title("Settings")]
    [SerializeField] private float timeBetweenPunishments = 10f;

    private float _timer;

    private void OnEnable()
    {
        AskForPunishment.OnEventRaised += TriggerRandomPunishment;
    }

    private void OnDisable()
    {
        AskForPunishment.OnEventRaised -= TriggerRandomPunishment;
    }

    private void Start()
    {
        _timer = timeBetweenPunishments;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            TriggerRandomPunishment();
            _timer = timeBetweenPunishments;
        }
    }

    [Button("Force Random Punishment")]
    public void TriggerRandomPunishment()
    {
        if (MalusPool.Count == 0) return;

        List<RSF_BaseMalus> temporaryPool = new List<RSF_BaseMalus>(MalusPool);
        bool punishmentApplied = false;

        while (!punishmentApplied && temporaryPool.Count > 0)
        {
            int randomIndex = Random.Range(0, temporaryPool.Count);
            RSF_BaseMalus selectedMalus = temporaryPool[randomIndex];

            punishmentApplied = selectedMalus.Execute();

            if (!punishmentApplied)
            {
                temporaryPool.RemoveAt(randomIndex);
            }

            Debug.Log(selectedMalus.name);
        }

        if (punishmentApplied)
        {
            Debug.Log("Le jeu a réussi à te nuire.");
        }
        else
        {
            Debug.Log("Le joueur est immunisé à toutes les punitions pour l'instant...");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Store;
using Services;
using System;
using Artifacts;
using Player;

public class ArtifactEffectApplier : MonoBehaviour
{
    [SerializeField] private EquippedItemsHandler _equippedItemsHandler;

    [Header("Appliers")]
    [SerializeField] private FlightDataHandler _flightDataHandler;


    private void OnEnable()
    {
        StartSessionHandler.SessionStarted += OnSessionStarted;
    }

    private void OnDisable()
    {
        StartSessionHandler.SessionStarted -= OnSessionStarted;
    }

    private void OnSessionStarted()
    {
        ApplyEffects(_equippedItemsHandler.GetFirstArtifact() as ScriptableArtifactData, 
            _equippedItemsHandler.GetSecondArtifact() as ScriptableArtifactData);
    }

    public void SetCoinValueMultiplier(int value) => _flightDataHandler.SetCoinValueMultiplier(value);
    public int GetCoinValueMultiplier() => _flightDataHandler.CoinValueMultiplier;

    private void ApplyEffects(ScriptableArtifactData storeItem1, ScriptableArtifactData storeItem2)
    {
        if (storeItem1 != null)
        {
            storeItem1.GetBaseArtifactEffect().GetEffect().Invoke(this);
        }
        if (storeItem2 != null)
        {
            storeItem2.GetBaseArtifactEffect().GetEffect().Invoke(this);
        }
    }
}

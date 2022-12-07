using System;
using System.Collections.Generic;
using Artifacts;
using Store;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class StartSessionHandler : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private ArtifactEffectApplier _artifactEffectApplier;
        [SerializeField] private List<GameObject> _objectsToDeactivate;

        public static event Action SessionStarted;
        
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            _artifactEffectApplier.ApplyEffects(EquippedItemsHandler.Instance.GetFirstArtifact(),
                EquippedItemsHandler.Instance.GetSecondArtifact());
            SessionStarted?.Invoke();
            foreach (var obj in _objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
        
    }
}